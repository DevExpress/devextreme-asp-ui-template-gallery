let passwordFormData = {
    currentPassword: "12345",
    password: "12345",
    confirmPassword: "12345"
}

const formatPhone = (value: string) => {
    return String(value).replace(/(\d{3})(\d{3})(\d{4})/, '+1($1)$2-$3');
};

function copyToClipboard(e: DevExpress.ui.dxButton.ClickEvent) {
    const tipText = 'Text copied';
    DevExpress.ui.notify(
        {
            message: tipText,
            minWidth: `${tipText.length + 2}ch`,
            width: 'auto',
            position: { of: e.element, offset: '0 -30' }
        },
        'info',
        500
    );
}

function handleChangePasswordClick(e: DevExpress.ui.dxButton.ClickEvent) {
    $("#change-password-popup").dxPopup("instance").show();
}

function onTCancel(e: DevExpress.ui.dxButton.ClickEvent) {
    $("#basic-info-id").dxForm("instance").reset();
    $("#contact-info-id").dxForm("instance").reset();
    $("#address-info-id").dxForm("instance").reset();

    $("#form-save-button-id").dxButton("instance").option("disabled", true);
    $("#form-cancel-button-id").dxButton("instance").option("disabled", true);
}

function onTSave(e: DevExpress.ui.dxButton.ClickEvent) {

}

function onScroll(e: DevExpress.ui.dxScrollView.ScrollEvent) {

}

function onCancelClick(e: DevExpress.ui.dxButton.ClickEvent) {
    $("#change-password-popup").dxPopup("instance").hide();
}

function onSaveClick(e: DevExpress.ui.dxButton.ClickEvent) {
    $("#change-password-popup").dxPopup("instance").hide();
    DevExpress.ui.notify({ message: 'Password Changed', position: { at: 'bottom center', my: 'bottom center' } }, 'success');
}

function passwordEyeClicked(e: DevExpress.ui.dxButton.ClickEvent) {
    const icon = e.component.option("icon");
    e.component.option("icon", icon == "eyeopen" ? "eyeclose" : "eyeopen");
    const textBox = $(e.element).closest(".dx-textbox").dxTextBox("instance");
    if (textBox) {
        const isPasswordMode = textBox.option("mode") === "password";
        textBox.option("mode", isPasswordMode ? "text" : "password");
    }
}

function onPasswordValueChanged(e: DevExpress.ui.dxTextBox.ValueChangedEvent) {
    const editor = $("#password-reset-id").dxForm("instance").getEditor("ConfirmPassword");
    if (editor?.option("value")) {
        editor?.element().dxValidator('validate');
    }
}

function comparePassword() {
    const editor = $("#password-reset-id").dxForm("instance").getEditor("Password");
    let value = "";
    if (editor?.option("value")) {
        value = editor?.option("value");
    }
    return value;
}

function formDataChanged(e: DevExpress.ui.dxForm.OptionChangedEvent) {
    if (e.component.option("isDirty")) {
        $("#form-save-button-id").dxButton("instance").option("disabled", false);
        $("#form-cancel-button-id").dxButton("instance").option("disabled", false);
    } else {
        $("#form-save-button-id").dxButton("instance").option("disabled", true);
        $("#form-cancel-button-id").dxButton("instance").option("disabled", true);
    }
}

function passwordResetOptionChanged(e: DevExpress.ui.dxForm.FieldDataChangedEvent) {
    const result = e.component.validate();
    if (result.isValid) {
        $("#reset-save-button").dxButton("instance").option("disabled", false);
    } else {
        $("#reset-save-button").dxButton("instance").option("disabled", true);
    }
}