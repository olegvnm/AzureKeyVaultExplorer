"use strict";

(function () {
    document.querySelectorAll("button").forEach(function (button) {
        return button.addEventListener("click", function () {
            var secretKeyText =
                button.parentElement.previousElementSibling.previousElementSibling
                    .innerText;
            var secretValueText =
                button.parentElement.previousElementSibling.innerText;
            navigator.clipboard.writeText([secretValueText]);
            Toastify({
                text: "'".concat(secretKeyText, "' copied! "),
                backgroundColor: "#6c7ae0",
                gravity: "bottom",
                position: "center",
                offset: {
                    y: 20
                },
                className: "toast"
            }).showToast();
        });
    });
})();