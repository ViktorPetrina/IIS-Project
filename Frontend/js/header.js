document.addEventListener("DOMContentLoaded", async () => {
    const headerContainer = document.getElementById("header");
    if (!headerContainer) return;

    const res = await fetch("header.html");
    const html = await res.text();
    headerContainer.innerHTML = html;

    const authLink = document.getElementById("auth-link");
    const accessToken = localStorage.getItem("accessToken");

    if (accessToken) {
        authLink.textContent = "Logout";
        authLink.href = "#";
        authLink.addEventListener("click", async (e) => {
            e.preventDefault();
            try {
                const refreshToken = localStorage.getItem("refreshToken");
                await fetch("http://localhost:5095/api/Auth/logout", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        "Authorization": `Bearer ${accessToken}`
                    },
                    body: JSON.stringify({ refreshToken })
                });
            } catch (err) {
                console.error("Logout failed:", err);
            }
            localStorage.removeItem("accessToken");
            localStorage.removeItem("refreshToken");
            window.location.href = "login.html";
        });
    } else {
        authLink.textContent = "Login";
        authLink.href = "login.html";
    }
});
