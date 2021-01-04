window.jsinteropFunc = {
    preventTabEvent: function() {
        document.getElementById("PreventTabTextArea").addEventListener("keydown", function (event) {
                if (event.keyCode=== 9) {
                    event.preventDefault();
                    var start = this.selectionStart;
                    var end = this.selectionEnd;

                    // set textarea value to: text before caret + tab + text after caret
                    this.value = this.value.substring(0, start) +
                        "\t" + this.value.substring(end);

                    // put caret at right position again
                    this.selectionStart = this.selectionEnd = start + 1;
                }
            }
        );
    }
}