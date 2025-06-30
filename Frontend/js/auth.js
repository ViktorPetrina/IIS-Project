document.addEventListener("DOMContentLoaded", () => {
    const accessToken = localStorage.getItem("accessToken");
    if (!accessToken) {
        window.location.href = "login.html";
        return;
    }

    if (isTokenExpired(accessToken)) {
        localStorage.removeItem("accessToken");
        window.location.href = "login.html";
    }
});

function isTokenExpired(token) {
    try {
        const payloadBase64Url = token.split('.')[1];
        const payloadBase64 = payloadBase64Url.replace(/-/g, '+').replace(/_/g, '/');
        const payloadJson = decodeURIComponent(atob(payloadBase64).split('').map(function(c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));
        const payload = JSON.parse(payloadJson);

        const currentTime = Math.floor(Date.now() / 1000);
        return payload.exp && currentTime >= payload.exp;
    } catch (e) {
        console.error('Failed to decode token', e);
        return true;
    }
}

