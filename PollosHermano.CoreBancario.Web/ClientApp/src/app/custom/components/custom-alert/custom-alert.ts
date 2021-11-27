import swal, { SweetAlertIcon } from "sweetalert2";

export function show(type: number, title: string, text: string, onClose: any) {
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
    confirmButtonText: "Aceptar",
    customClass: {
      confirmButton: "btn btn-primary"
    },
    buttonsStyling: false,
  }).then(function () {
    if (onClose) {
      onClose();
    }
  });
}
