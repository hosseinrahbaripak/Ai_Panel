function ShowToast(status = 'success', message = '' , time=5000) {
    const container = document.querySelector('.toast-container');

    const toast = document.createElement('div');
    toast.className = 'toast';

    let progressColor = '#28a745'; 
    if (status === 'error') progressColor = '#dc3545';
    else if (status === 'warning') progressColor = '#ffc107';

    toast.innerHTML = `
        <div class="toast-content">
            ${message}
            <div class="progress-bar" style="background-color: ${progressColor};"></div>
        </div>
    `;
    container.appendChild(toast);

    setTimeout(() => toast.classList.add('show'), 100);

    const progress = toast.querySelector('.progress-bar');

    setTimeout(() => progress.style.width = '100%', 150);

    setTimeout(() => {
        toast.classList.remove('show');
        toast.style.transform = 'translateX(100%)';
        toast.style.opacity = '0';
    }, time);

    setTimeout(() => toast.remove(), time+500);
}
