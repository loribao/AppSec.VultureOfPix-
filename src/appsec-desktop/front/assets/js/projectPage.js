document.addEventListener("DOMContentLoaded", function () {
    var openFormLink = document.getElementById("openForm");
    var formPopOut = document.getElementById("formPopOut");
    var closeFormButton = document.getElementById("closeForm");

    function openFormPopOut() {
        formPopOut.style.display = "block";
    }

    function closeFormPopOut() {
        formPopOut.style.display = "none";
    }

    openFormLink.addEventListener("click", openFormPopOut);

    closeFormButton.addEventListener("click", closeFormPopOut);

    var dropdownItems = document.querySelectorAll(".dropdownContent a");
    dropdownItems.forEach(function (item) {
        item.addEventListener("click", function () {
            var dropbtn = document.querySelector(".dropbtn");
            dropbtn.textContent = this.textContent;
            var dropdownContent = document.getElementById("dropdownContent");
            dropdownContent.classList.remove("show");
        });
    });
});

function toggleDropdown() {
    var dropdownContent = document.getElementById("dropdownContent");
    dropdownContent.classList.toggle("show");
}

window.onclick = function (event) {
    if (!event.target.matches(".dropbtn")) {
        var dropdowns = document.getElementsByClassName("dropdownContent");
        for (var i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains("show")) {
                openDropdown.classList.remove("show");
            }
        }
    }
};

document.addEventListener("DOMContentLoaded", function () {
    var avatarDropdown = document.getElementById("avatarDropdown");
    var dropdownContent = avatarDropdown.querySelector(".dropDownContent");

    avatarDropdown.addEventListener("click", function () {
        dropdownContent.style.display =
            dropdownContent.style.display === "block" ? "none" : "block";
    });
});

document.addEventListener("DOMContentLoaded", function () {
    document
        .getElementById("showMenuBtn")
        .addEventListener("click", function () {
            document
                .querySelector(".mainMenuContainer")
                .classList.remove("hidden");
            document.getElementById("hideMenuBtn").classList.remove("hidden");
            this.classList.add("hidden");
        });

    document
        .getElementById("hideMenuBtn")
        .addEventListener("click", function () {
            document
                .querySelector(".mainMenuContainer")
                .classList.add("hidden");
            document.getElementById("showMenuBtn").classList.remove("hidden");
            this.classList.add("hidden");
        });
});
