(function() {
    document
        .querySelectorAll("button")
        .forEach(button =>
            button.addEventListener(
                "click",
                function() {
                    var secretKeyText = button.parentElement.previousElementSibling.previousElementSibling.innerText;
                    var secretValueText = button.parentElement.previousElementSibling.innerText;
                    navigator.clipboard.writeText([secretValueText]);

                    Toastify({
                        text: `'${secretKeyText}' copied! `,
                        backgroundColor: "#6c7ae0",
                        gravity: "bottom",
                        position: 'center',
                        offset: {
                            y: 20
                        },
                        className: "toast"
                    }).showToast();
                }));
}())