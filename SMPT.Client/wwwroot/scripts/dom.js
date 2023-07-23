window.MyInterop = (function () {
    const _changeTheme = function (isDark) {
        if (isDark)
            document.body.classList.add('dark-theme');
        else
            document.body.classList.remove('dark-theme');
    };

    return {
        ChangeTheme: _changeTheme
    };
})();