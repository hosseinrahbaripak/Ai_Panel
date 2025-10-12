document.addEventListener('DOMContentLoaded', function () {
    // convert math formulas to palin text (LaTex)
    const forms = document.querySelectorAll('.mathEditorContainer');
    forms.forEach(form => {
        form.addEventListener('submit', function (e) {
            const editors = form.querySelectorAll('.mathEditor');

            editors.forEach(editor => {
                const outputId = editor.dataset.output;
                const outputInput = document.getElementById(outputId);
                if (!outputInput) return;
                let result = '';
                editor.childNodes.forEach(node => {
                    if (node.nodeType === Node.ELEMENT_NODE && node.tagName === 'MATH-FIELD') {
                        try {
                            const latex = node.value || node.getAttribute('value') || '';
                            result += `$${latex}$`;
                        } catch (e) {
                            console.warn('Failed to extract value from math-field:', node, e);
                        }
                    } else {
                        result += node.textContent;
                    }
                });
                outputInput.value = result.trim();
            });
        });
	});
    // Initialize editors from hidden input if needed
    const editors = document.querySelectorAll('.mathEditor');
    editors.forEach(editor => {
        if (editor.innerHTML.trim() !== '') return;

        const outputId = editor.dataset.output;
        const hiddenInput = document.getElementById(outputId);
        if (!hiddenInput || hiddenInput.value.trim() === '') return;

        const rawText = hiddenInput.value;
        const regex = /\$(.+?)\$/g;

        let lastIndex = 0;
        editor.innerHTML = ''; // Clear just in case
        let match;

        while ((match = regex.exec(rawText)) !== null) {
            const textBefore = rawText.substring(lastIndex, match.index);
            if (textBefore) {
                editor.appendChild(document.createTextNode(textBefore));
            }

            const mathField = document.createElement("math-field");
            mathField.setAttribute("style", "display:inline-block; min-width: 150px; margin: 0 4px;");
            mathField.setAttribute("read-aloud", "");
            mathField.setOptions?.({
                smartFence: true,
                virtualKeyboardMode: 'onfocus',
                locale: 'fa',
            });
            mathField.value = match[1];

            editor.appendChild(mathField);
            const spacer = document.createTextNode("\u00A0"); // non-breaking space
            editor.appendChild(spacer);
            lastIndex = match.index + match[0].length;
        }

        const remainingText = rawText.substring(lastIndex);
        if (remainingText) {
            editor.appendChild(document.createTextNode(remainingText));
        }
    });
    renderMathDisplays();

});
function insertFormula(editorId) {
    const editor = document.getElementById(editorId);

    // Create the math-field element
    const mathField = document.createElement("math-field");
    mathField.setAttribute("style", "display:inline-block; min-width: 150px; margin: 0 4px;");
    mathField.setAttribute("read-aloud", ""); // optional
    mathField.setOptions?.({
        smartFence: true,
        virtualKeyboardMode: 'onfocus',
        locale: 'fa',
    });

    // Create a spacer element after the math-field
    const spacer = document.createTextNode("\u00A0"); // non-breaking space

    // Insert the math field at the current caret (cursor) position
    const selection = window.getSelection();
    if (!selection.rangeCount) return;

    const range = selection.getRangeAt(0);

    // Ensure selection is inside the editor
    if (!editor.contains(range.commonAncestorContainer)) {
        editor.focus();
        return;
    }

    range.deleteContents();
    range.insertNode(spacer);
    range.insertNode(mathField);

    // Move cursor after the spacer
    range.setStartAfter(spacer);
    range.collapse(true);

    selection.removeAllRanges();
    selection.addRange(range);

    mathField.focus();
}


function renderMathDisplays() {
    document.querySelectorAll('.mathDisplay').forEach(container => {
        container.setAttribute('dir', 'rtl');

        let text = container.innerHTML;

        text = text.replace(/\$(.+?)\$/g, (match, latex) => {
            try {
                const html = MathLive.convertLatexToMarkup(latex.trim(), {
                    mathstyle: 'text',
                });
                return `<span dir="ltr" style="display:inline-block;">${html}</span>`;
            } catch (e) {
                console.error('LaTeX render error:', e);
                return match;
            }
        });

        container.innerHTML = text;
    });

    MathLive.renderMathInElement(document.body, {
        delimiters: [{ left: "$", right: "$", mode: "math" }]
    });
}
