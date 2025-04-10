

const formatPhone = (value: string) => {
    return String(value).replace(/(\d{3})(\d{3})(\d{4})/, '+1($1)$2-$3');
};

function copyToClipboard(e: DevExpress.ui.dxButton.ClickEvent) {

}

function handleChangePasswordClick(e: DevExpress.ui.dxButton.ClickEvent) {
    $("#change-password-popup").dxPopup("instance").show();
}

function onTCancel(e: DevExpress.ui.dxButton.ClickEvent) {

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
