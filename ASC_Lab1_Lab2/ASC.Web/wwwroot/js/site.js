document.addEventListener('DOMContentLoaded', function () {
    const sideNav = document.querySelectorAll('.sidenav');
    M.Sidenav.init(sideNav);

    const parallax = document.querySelectorAll('.parallax');
    M.Parallax.init(parallax);

    const selects = document.querySelectorAll('select');
    M.FormSelect.init(selects);

    const textareas = document.querySelectorAll('.materialize-textarea');
    textareas.forEach(function (item) {
        M.textareaAutoResize(item);
    });
});
