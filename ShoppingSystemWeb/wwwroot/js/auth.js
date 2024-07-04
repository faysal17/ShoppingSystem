
$(document).ready(function () {
    $("#register-form").hide();
});

let registerButtonClicked = () => {
    $("#register-form").show();
    $("#login-form").hide();
}

let loginButtonClicked = () => {
    $("#register-form").hide();
    $("#login-form").show();
}

let roleChanged = (selectElement) => {
    $("#role").val(selectElement.value);
}