function parseJwt(token) {
    try {
        const base64Url = token.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        return JSON.parse(decodeURIComponent(escape(window.atob(base64))));
    } catch { return null; }
}
function currentUser() {
    const t = getToken(); if (!t) return null;
    const p = parseJwt(t); if (!p) return null;
    const role = p.role || p["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || "";
    const email = p.sub || p.unique_name || "";
    return { email, role };
}

$(function () {
    const u = currentUser();
    const $auth = $("#authArea");
    if (!$auth.length) return;

    if (u) {
        $auth.html(`
      <span class="me-2">Hello, ${u.email} (${u.role})</span>
      <button id="btnLogout" class="btn btn-outline-light btn-sm">Logout</button>
    `);
        $("#btnLogout").on("click", function () {
            clearToken();
            location.href = "/Account/Login";
        });
    } else {
        $auth.html(`<a class="btn btn-outline-light btn-sm" href="/Account/Login">Login</a>`);
    }
});
