document.addEventListener("DOMContentLoaded", function () {
    var openFormLink = document.getElementById("openForm");
    var formPopOut = document.getElementById("menuSidebar");
    var closeFormButton = document.getElementById("closeForm");

    function openFormPopOut() {
        formPopOut.classList.add("open");
    }

    function closeFormPopOut() {
        formPopOut.classList.remove("open");
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

    var avatarDropdown = document.getElementById("avatarDropdown");
    var dropdownContent = avatarDropdown.querySelector(".dropDownContent");

    avatarDropdown.addEventListener("click", function () {
        dropdownContent.style.display =
            dropdownContent.style.display === "block" ? "none" : "block";
    });

    $("#showMenuBtn").click(function () {
        $(".mainMenuContainer").removeClass("hidden");
        $("#hideMenuBtn").removeClass("hidden");
        $(this).addClass("hidden");
    });

    $("#hideMenuBtn").click(function () {
        $(".mainMenuContainer").addClass("hidden");
        $("#showMenuBtn").removeClass("hidden");
        $(this).addClass("hidden");
    });
});

document.addEventListener("DOMContentLoaded", function () {
    var dropdownItems = document.querySelectorAll(".dropdownContent a");

    dropdownItems.forEach(function (item) {
        item.addEventListener("click", function () {
            var dropbtn = document.querySelector(".dropbtn");
            dropbtn.textContent = this.textContent;
            var dropdownContent = document.querySelector(".dropdownContent");
            dropdownContent.classList.remove("show");
        });
    });

    var dropbtn = document.querySelector(".dropbtn");
    var dropdownContent = document.querySelector(".dropdownContent");

    dropbtn.addEventListener("click", function () {
        dropdownContent.classList.toggle("show");
        var rect = dropbtn.getBoundingClientRect();
        dropdownContent.style.bottom = window.innerHeight - rect.top + "px"; // Ajuste aqui
    });

    // Fechar o dropdown ao clicar fora dele
    window.addEventListener("click", function (event) {
        if (!event.target.matches(".dropbtn")) {
            var dropdowns = document.querySelectorAll(".dropdownContent");
            dropdowns.forEach(function (dropdown) {
                if (dropdown.classList.contains("show")) {
                    dropdown.classList.remove("show");
                }
            });
        }
    });
});

document.addEventListener("DOMContentLoaded", function () {
    var showMenuBtn = document.getElementById("showMenuBtn");
    var hideMenuBtn = document.getElementById("hideMenuBtn");
    var mainMenuContainer = document.querySelector(".mainMenuContainer");

    showMenuBtn.addEventListener("click", function () {
        mainMenuContainer.classList.remove("hidden");
        hideMenuBtn.classList.remove("hidden");
        showMenuBtn.classList.add("hidden");
    });

    hideMenuBtn.addEventListener("click", function () {
        mainMenuContainer.classList.add("hidden");
        hideMenuBtn.classList.add("hidden");
        showMenuBtn.classList.remove("hidden");
    });
});

function togglePasswordVisibility() {
    var senhaInput = document.getElementById("senhaInput");
    var passwordToggle = document.querySelector(".passwordToggle");

    if (senhaInput.type === "password") {
        senhaInput.type = "text";
        passwordToggle.classList.add("hidePassword");
    } else {
        senhaInput.type = "password";
        passwordToggle.classList.remove("hidePassword");
    }
}

document.addEventListener("DOMContentLoaded", function () {
    var emailInput = document.querySelector('input[type="email"]');
    var passwordInput = document.getElementById("senhaInput");

    emailInput.addEventListener("input", function () {
        var inputValue = emailInput.value;
        if (inputValue) {
            emailInput.style.color = "#ffff";
        } else {
            emailInput.style.color = "";
        }
    });

    passwordInput.addEventListener("input", function () {
        var inputValue = passwordInput.value;
        if (inputValue) {
            passwordInput.style.color = "#ffff";
        } else {
            passwordInput.style.color = "";
        }
    });
});
