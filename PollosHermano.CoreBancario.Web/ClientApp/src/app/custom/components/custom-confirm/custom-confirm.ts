import swal, { SweetAlertIcon } from "sweetalert2";

export function show(type: number, title: string, text: string, onConfirm: any, onCancel: any) {
  let sType: SweetAlertIcon;

  switch (type) {
    case 1:
      sType = "success";
      break;
    case 2:
      sType = "warning";
      break;
    case 3:
      sType = "error";
      break;
    default:
      sType = "info";
      break;
  }

  swal.fire({
    title: title ? title : "",
    text: text ? text : "",
    icon: sType,
    showCancelButton: true,
    confirmButtonColor: "#2F8BE6",
    cancelButtonColor: "#F55252",
    confirmButtonText: "Aceptar",
    cancelButtonText: "Cancelar",
    customClass: {
      confirmButton: "btn btn-primary",
      cancelButton: "btn btn-danger ml-1"
    },
    buttonsStyling: false,
  }).then(function (result) {
    if (result.value) {
      if (onConfirm) {
        onConfirm();
      }
    }
    else {
      if (onCancel) {
        onCancel();
      }
    }
  });
}
