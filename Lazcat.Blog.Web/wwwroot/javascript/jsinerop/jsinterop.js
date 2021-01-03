window.jsinteropFunc = {
    preventTabEvent: function() {
        document.getElementById("PreventTabTextArea").addEventListener("keydown",function (event) {
                if (event.keyCode=== 9) {
                    event.preventDefault();
                }
            }
        );
    }
}