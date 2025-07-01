(function () {
    if (window.uitgAppContext?.UserProfileController) return;

    let passwordFormData = {
        currentPassword: "12345",
        password: "12345",
        confirmPassword: "12345"
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

        const saveButton = $("#form-save-button-id").dxButton("instance");
        const cancelButton = $("#form-cancel-button-id").dxButton("instance");
        saveButton.option("disabled", true);
        cancelButton.option("disabled", true);
    }

    function onTSave(e: DevExpress.ui.dxButton.ClickEvent) {
        // Implementation for save
    }

    function onScroll(e: DevExpress.ui.dxScrollView.ScrollEvent) {
        // Implementation for scroll
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
        const saveButton = $("#form-save-button-id").dxButton("instance");
        const cancelButton = $("#form-cancel-button-id").dxButton("instance");
        const isDirty = e.component.option("isDirty");
        saveButton.option("disabled", !isDirty);
        cancelButton.option("disabled", !isDirty);
    }

    function passwordResetOptionChanged(e: DevExpress.ui.dxForm.FieldDataChangedEvent) {
        const result = e.component.validate();
        const resetSaveButton = $("#reset-save-button").dxButton("instance");
        resetSaveButton.option("disabled", !result.isValid);
    }

    // Optionally, you can expose these as a controller object
    window.uitgAppContext.UserProfileController = {
        passwordFormData,
        copyToClipboard,
        handleChangePasswordClick,
        onTCancel,
        onTSave,
        onScroll,
        onCancelClick,
        onSaveClick,
        passwordEyeClicked,
        onPasswordValueChanged,
        comparePassword,
        formDataChanged,
        passwordResetOptionChanged
    };
})();
