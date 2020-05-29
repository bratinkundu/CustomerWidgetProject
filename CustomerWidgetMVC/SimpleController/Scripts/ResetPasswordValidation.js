function ValidateConfirmPassword() {
    var password = document.getElementById("password").value;
    var confirmPassword = document.getElementById("cpassword").value;
    if (password != confirmPassword) {
        return false;
    }
    return true;
}

function ValidatePassword() {
    var userinput = $("#password").val();
    var pattern = /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,}$/;
    if (!pattern.test(userinput)) {
        return false;
    }
    return true;
}


function ResetPassword() {
    
    var val = ValidatePassword();
    if (val == false) {
        alert("password contain 1 special character, Upper-Lower case, Number, minimum length 8");
        return false;
    }

    var val = ValidateConfirmPassword();
    if (val == false) {
        alert("Passwords do not match.");
        return false;
    }
    console.log($('#password').val());
    $('#password').val(CryptoJS.MD5($('#password').val()).toString());
    $('#cpassword').val(CryptoJS.MD5($('#cpassword').val()).toString());
    console.log($('#password').val());
    //alert($('#password').val());

}