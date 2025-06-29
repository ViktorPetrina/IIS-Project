document.addEventListener("DOMContentLoaded", () => {
    const accessToken = localStorage.getItem("accessToken");
    if (!accessToken) {
        window.location.href = "login.html";
    }
});
