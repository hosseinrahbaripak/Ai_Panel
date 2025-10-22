using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ai_Panel.Pages.Admin
{
    public class ContractModel(IGenericRepository<TestAiConfig> _testAiConfig) : PageModel
    {
        public string? JsonData { get; set; }

        public async Task<IActionResult> OnGetAsync(int Id)
        {
            var config = await _testAiConfig.Get(Id);

            if (config == null || string.IsNullOrWhiteSpace(config.AiResponse))
                return Page();

            var match = Regex.Match(config.AiResponse, @"\{[\s\S]*\}");
            if (match.Success)
            {
                JsonData = match.Value;
                var template = GetTemplate();
                var filledContract = FillTemplate(template, JsonData);
                ViewData["Contract"] = filledContract;
            }

            return Page();
        }



        private string FillTemplate(string template, string jsonData)
        {
            try
            {
                var jsonNode = JsonNode.Parse(jsonData);

                var regex = new Regex(@"\{([^\{\}]+)\}");
                var result = regex.Replace(template, match =>
                {
                    string path = match.Groups[1].Value;
                    var keys = path.Split('.');
                    JsonNode? currentNode = jsonNode;

                    foreach (var key in keys)
                    {
                        if (currentNode == null) break;
                        currentNode = currentNode[key];
                    }

                    if (currentNode == null || string.IsNullOrWhiteSpace(currentNode.ToString()))
                        return "";

                    return currentNode.ToString() ?? "";
                });

                return result;
            }
            catch
            {
                return template;
            }
        }


        private string GetTemplate()
        {
            string temp = @"
            1-1- موجر/ موجرين {lessor.full_name} فرزند {lessor.father_name} به شماره شناسنامه {lessor.id_number} صادره از {lessor.id_issuer} کدملي {lessor.national_id} متولد {lessor.birth_place} ساکن {lessor.address} تلفن {lessor.phone} با وکالت/قيمومت/ولايت/وصايت {lessee.full_name} فرزند {lessee.father_name} به شماره شناسنامه {lessee.id_number} متولد {lessee.birth_place} بموجب {lessor.representation_document}  
2-1- مستأجر/ مستأجرين {lessee.full_name} فرزند {lessee.father_name} به شماره شناسنامه {lessee.id_number} صادره از {lessee.id_issuer} کدملي {lessee.national_id} متولد {lessee.birth_place} ساکن {lessee.address} تلفن {lessee.phone} با وکالت/قيمومت/ولايت/وصايت {lessee.representation_type} فرزند {lessee.father_name} به شماره شناسنامه {lessee.id_number} متولد {lessee.birth_place} بموجب {lessee.representation_document}  

ماده 2: موضوع قرارداد و مشخصات مورد اجاره عبارتست از تمليک منافع {property.ownership_fraction} دانگ / دستگاه / يکباب {property.subdivision} واقع در {property.address} داراي پلاک ثبتي شماره {property.registry_number} فرعي از {property.registry_sub} اصلي {property.registry_main} بخش {property.registry_section} به مساحت {property.area_square_meters} متر مربع داراي سند مالکيت بشماره سريال {property.ownership_serial} صفحه {property.ownership_page} دفتر {property.ownership_book} بنام {property.owner_name} مشتمل بر {property.rooms} اتاق خواب با حق استفاده برق / آب / گاز بصورت اختصاصي/ اشتراکي/ شوفاژ {property.facilities.heating_system}/ کولر {property.facilities.cooler}/ پارکينگ {property.facilities.parking_area_m2} فرعی به متراژ {property.facilities.parking_area_m2} مترمربع / انباري فرعی {property.facilities.warehouse_area_m2} به متراژ {property.facilities.warehouse_area_m2} متر مربع / تلفن داير/ غير داير به شماره {property.facilities.telephone} و ساير لوازم و منصوبات و مشاعات مربوطه که جهت استفاده به رويت مستأجر / مستأجرين رسيده و مورد قبول قرار گرفته است .  

ماده 3 : مدت اجاره  
مدت اجاره {contract.duration_months} ماه / سال شمسي از تاريخ {contract.start_date} الي {contract.end_date} مي باشد .  

ماده 4 : اجاره بها و نحوه پرداخت  
1-4- ميزان اجاره بها جمعاً {contract.total_rent} ريال ، از قرار ماهيانه مبلغ {contract.monthly_rent} ريال که در اول / آخر هر ماه به موجب قبض رسيد پرداخت مي شود .  
2-4- مبلغ {contract.deposit_amount} ريال از طرف مستأجر / مستأجرين بعنوان قرض الحسنه نقداً / طي چــک به شمـــاره {contract.deposit_check_number} بانک {contract.deposit_bank} شعبه {contract.deposit_branch} پرداخـــت شـــد و يا نقداً / طي چـــک به شمـــاره {contract.deposit_check_number} بانک {contract.deposit_bank} شعبه {contract.deposit_branch} در تاريخ {contract.deposit_return_date} به موجر پرداخت خواهد شد . معادل مبلغ پرداختي به عنوان قرض الحسنه با انقضاء مدت اجاره و يا فسخ آن همزمان با تخليه عين مستأجره به مستأجر مسترد خواهد شد .  

ماده 5 : تسليم مورد اجاره  
موجر مکلف است در تاريخ {contract.handover_date} مورد اجاره را با تمام توابع و ملحقات آن جهت استيفاء به مستأجر / مستأجرين تسليم کند .  

ماده 6 : شرايط و آثار قرارداد  
1-6- مستأجر نمي تواند از مورد اجاره بر خلاف منظور قرارداد ({contract.usage_type}) استفاده نمايد . مستأجر مکلف است به نحو متعارف از مورد اجاره استفاده نمايد .  
2-6- مستأجر حق استفاده از مورد اجاره را به نحو مباشرت دارد و بايستي شخصاً از مورد اجاره استفاده کند . علاوه بر اين مستأجر حق انتقال و واگذاري مورد اجاره را به غير دارد / ندارد ({contract.sublease_permission}) . در صورت تخلف و انتقال به غير ، اعتبار اجاره منوط به تنفيذ مالک خواهد بود در صورتيکه مستأجر مورد اجاره را به غير بدون اذن مالک تسليم کرده باشد شخص متصرف در صورت عدم تنفيذ در برابر مالک در حدود مقررات قانوني مسئول خواهد بود .  
3-6- موجر بايد مالک يا متصرف قانوني يا قراردادي مورد اجاره باشد و در صورتيکه به عنوان ولايت ، وصايت ، وکالت ، قيمومت ، اجاره با حق انتقال و از اين قبيل اختيار اجاره دادن داشته باشد ، بايستي اسناد و مدارک مثبت حق انتقال را ضميمه کند و مشاور موظف به احراز موضوع فوق است .  
4-6- در صورتيکه مستأجر از پرداخت اجاره بها بيش از يک ماه تاخير نمايد ، موجر مي تواند قرارداد را فسخ و تخليه مورد اجاره را از مراجع ذيصلاح بخواهد .  
5-6- پرداخت هزينه هاي مصرفي آب / برق / گاز / تلفن / شارژ / فاضلاب شهري بر عهده مستأجر است و بايد در موعد تخليه يا فسخ قبوض پرداختي را به موجر ارائه نمايد ({contract.utilities.water}, {contract.utilities.electricity}, {contract.utilities.gas}, {contract.utilities.telephone}, {contract.utilities.waste}, {contract.utilities.building_charge}) .  
6-6- پرداخت هزينه نگهداري آپارتمان (حق شارژ و غيره) و همچنين افزايش احتمالي آن بر مبناي مصوب مسئول يا مسئولين ساختمان بر عهده مستأجر است .  
7-6- پرداخت هزينه تعميرات و هزينه هاي کلي از قبيل نصب و راه اندازي به منظور بهره برداري از دستگاه تهويه ، شوفاژ ، کولر ، آسانسور و شبکه آب ، برق و گاز به عهده مالک است و هزينه هاي جزيي مربوط به استفاده از مورد اجاره به عهده مستأجر است و نوع هزينه ها و ميزان آن را عرف تعيين مي کند ({contract.major_repairs_by}, {contract.minor_repairs_by}) .  
8-6- ماليات مستغلات و تعميرات اساسي و عوارض شهرداري با موجر است و ماليات بر درآمد و مشاغل ( تجاري ، اداري ) بر عهده مستأجر ({contract.taxes.property_tax}, {contract.taxes.income_tax}) .  
9-6- در خصوص اماکن تجاري مبلغ {contract.key_money_amount} ريال به حروف {contract.key_money_amount_text} بعنوان حق سرقفلي توسط مستأجر به موجرتسليم و پرداخت گرديد / نگرديده است .  
10-6- مستأجر مکلف است در زمان تخليه ، مورد اجاره را به همان وضعي که تحويل گرفته به موجر تحويل داده و رسيد دريافت نمايد . در صورت حدوث خسارات نسبت به عين مستأجره ، مستأجر متعهد به جبران خسارت وارده خواهد بود .  
11-6- موجر ملزم است در زمان تخليه نهايي و با تسويه حساب بدهيهاي زمان اجاره ، نسبت به استرداد قرض الحسنه دريافتي از مستأجر ، با اخذ رسيد اقدام نمايد .  
12-6- در صورتيکه موجر نسبت به پرداخت هزينه هايي که موجب انتفاع مستأجر از ملک مي باشد ، اقدام نکند و به مستأجر نيز اجازه انجام تعميرات لازم ندهد ، مستأجر مي تواند شخصاً نسبت به انجام تعميرات مربوطه اقدام و هزينه هاي مربوطه را با موجر محاسبه کند .  
13-6- تمديد قرارداد اجاره فقط با توافق کتبي طرفين قبل از انقضاء مدت قرارداد ممکن است . در صورت تمديد قرارداد اجاره الحاقي با شرايط و تغييرات مورد توافق بخش لاينفکي از قرارداد اجاره خواهد بود .  
14-6- مستأجر مکلف است به محض اتمام مدت اجاره ، عين مستأجره را بدون هيچگونه عذر و بهانه اي تخليه و به موجر تسليم نمايد ، چنانچه مستأجر مورد اجاره را رأس تاريخ انقضاء تخليه کامل ننمايد و يا به هر دليلي از تسليم آن به موجر خودداري نمايد موظف است روزانه مبلغ {contract.late_fee_per_day} ريال بعنوان اجرت المثل ايام تصرف بعد از اتمام قرارداد به موجر بپردازد و تهاتر خسارت ناشي از تاخير تخليه ( اجرت المثل ) با مبلغ قرض الحسنه بلا اشکال است .  

ماده 7 کليه خيارات ولو خيار غبن به استثناء خيار تدليس از طرفين ساقط گرديد .  
ماده 8 اين قرارداد در ساير موارد تابع مقررات قانون مدني و قانون روابط موجر و مستأجر مصوب سال 1376 خواهد بود .  
ماده 9 باستناد ماده 2 قانون روابط موجر و مستأجر مصوب سال 1376 شهود با مشخصات زير اين قرارداد را امضاء و گواهي مي نمايند :  
شاهد اول: {witnesses[0].full_name}، ساکن {witnesses[0].address}، تلفن {witnesses[0].phone}  
شاهد دوم: {witnesses[1].full_name}، ساکن {witnesses[1].address}، تلفن {witnesses[1].phone}  

ماده 10 حق الزحمه مشاور املاك طبق تعرفه كميسيون نظارت شهرستان {real_estate_office.office_name} به طور جداگانه به عهده طرفین است که همزمان با امضاء این قرارداد مبلغ {real_estate_office.commission_amount} ریال پرداخت شده و رسید دریافت نمایند . فسخ یا اقاله قرارداد تأثیری در میزان حق الزحمه نخواهد داشت .  

ماده 11 اين قرارداد در تاريخ {real_estate_office.registration_date} ساعت {contract.signing_time} در دفتر مشاور املاک {real_estate_office.office_name} به نشاني {real_estate_office.office_address} در سه نسخه بين طرفين تنظيم ، امضاء و مبادله گرديد . مشاور املاک مکلف است نسخ قرارداد را ممهور به مهر مشاور و نسخه اول و دوم را به موجر و مستأجر تسليم نمايد و نسخه سوم را در دفتر مخصوص بايگاني کند و هر سه نسخه داراي اعتبار واحد است .  

ماده 12 موارد حقوقي مفاد اين قرارداد مطابق با مقررات جاري است و تاييد مي شود .  
کارشناس حقوقی: {legal_expert.full_name}، توضیحات: {legal_expert.comments}  
یادداشت‌ها: {contract.notes}

            ";



            return temp;
        }
    }


}
