function showSuccessToast(message) {
    if (Swal) {
        return Swal.fire({
            text: message,
            icon: 'success',
            toast: true,
            position: 'top-end',
            showConfirmButton:false,
            timer: 3000
        })
    }
}

function showErrorToast(message) {
    if (Swal) {
        return Swal.fire({
            text: message,
            icon: 'error',
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 3000
        })
    }
}
function showPopupConfirm(title, message, confirmButtonText, cancelButtonText, callbackConfirm, callbackCancel) {
    if (Swal) {
        return Swal.fire({
            title: title,
            html: message,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: confirmButtonText,
            cancelButtonText: cancelButtonText,
            confirmButtonColor: '#E72929',
            cancelButtonColor: '#DDDDDD',
            reverseButtons: true,
            allowOutsideClick: false
        }).then(result => {
            if (result.isConfirmed) {
                if (typeof callbackConfirm == 'function') {
                    callbackConfirm()
                }
            }
            else {
                if (typeof callbackCancel == 'function') {
                    callbackCancel()
                }
            }
        })
    }
}
