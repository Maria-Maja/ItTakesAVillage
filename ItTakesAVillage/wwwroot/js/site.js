// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code

function handleAccordionClick(notificationId) {
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "/Notification?handler=HandleAccordionClick", true);
    xhr.setRequestHeader("Content-Type", "application/json");

    var antiForgeryToken = document.getElementsByName("__RequestVerificationToken")[0].value;
    xhr.setRequestHeader("RequestVerificationToken", antiForgeryToken);
    console.log("NotificationId sent to server:", notificationId);

    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            if (xhr.status === 200) {
                var response = JSON.parse(xhr.responseText);
                if (response.success) {
                    console.log("Success:", response);

                    var accordionElement = document.getElementById("collapse_" + notificationId);
                    accordionElement.innerHTML = "Uppdaterat innehåll"; // Uppdatera med rätt innehåll från response om det behövs
                } else {
                    console.error("Server responded with an error:", response);
                }
            } else {
                console.error("AJAX request failed with status:", xhr.status);
            }
        }
    };

    var data = JSON.stringify(notificationId);
    xhr.send(data);
}