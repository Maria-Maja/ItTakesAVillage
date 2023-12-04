function handleAccordionClick(notificationId) {
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "/Notification?handler=HandleAccordionClick", true);
    xhr.setRequestHeader("Content-Type", "application/json");

    var antiForgeryToken = document.getElementsByName("__RequestVerificationToken")[0].value;
    xhr.setRequestHeader("RequestVerificationToken", antiForgeryToken);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            if (xhr.status === 200) {
                var response = JSON.parse(xhr.responseText);
                if (response.success) {

                    var accordionButton = document.querySelector(`button[data-bs-target="#collapse_${notificationId}"]`);
                    //var accordionElement = document.getElementById("collapse_" + notificationId);

                    accordionButton.classList.remove("fw-bold"); 
                    //accordionElement.innerHTML = "Uppdaterat innehåll"; 

                    updateNotificationLink(response.unreadCount);
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

function updateNotificationLink(unreadCount) {
    var notificationLink = document.getElementById("notification-link");
    if (notificationLink) {
        notificationLink.innerHTML = `
            <i class="bi bi-bell"></i>
            ${unreadCount > 0 ? `<span class="badge bg-danger">${unreadCount}</span>` : ''}
        `;
    }
}