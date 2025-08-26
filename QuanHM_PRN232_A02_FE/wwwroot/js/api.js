function getToken() { return localStorage.getItem("token"); }
function setToken(t) { localStorage.setItem("token", t); }
function clearToken() { localStorage.removeItem("token"); }

function authHeader() {
    const t = getToken();
    return t ? { "Authorization": `Bearer ${t}` } : {};
}
function defaultErr(xhr) {
    let msg = `Error ${xhr.status}`;
    if (xhr.responseJSON && xhr.responseJSON.message) msg += `: ${xhr.responseJSON.message}`;
    alert(msg);
}

function apiGet(url, onOk, onErr) {
    $.ajax({
        url: `${API_BASE}/${url}`,
        type: "GET",
        headers: authHeader(),
        success: onOk,
        error: onErr || defaultErr
    });
}
function apiPost(url, data, onOk, onErr) {
    $.ajax({
        url: `${API_BASE}/${url}`,
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(data || {}),
        headers: authHeader(),
        success: onOk,
        error: onErr || defaultErr
    });
}
function apiPut(url, data, onOk, onErr) {
    $.ajax({
        url: `${API_BASE}/${url}`,
        type: "PUT",
        contentType: "application/json",
        data: JSON.stringify(data || {}),
        headers: authHeader(),
        success: onOk,
        error: onErr || defaultErr
    });
}
function apiDelete(url, onOk, onErr) {
    $.ajax({
        url: `${API_BASE}/${url}`,
        type: "DELETE",
        headers: authHeader(),
        success: onOk,
        error: onErr || defaultErr
    });
}
