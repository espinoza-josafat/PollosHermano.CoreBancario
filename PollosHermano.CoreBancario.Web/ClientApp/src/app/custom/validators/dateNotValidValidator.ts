import { ValidationErrors, ValidatorFn, AbstractControl } from "@angular/forms";
import Utils from "../../shared/helpers/Utils";
import * as moment from "moment";

export function dateNotValidValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        //debugger
        
        if (Utils.IsValidStringNotWhiteSpace(control.value)) {
            if (!(/^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/.test(control.value))) {
                return {
                    datenotvalid: {
                        value: control.value, 
                        format: "dd/mm/yyyy",
                        message: "Este campo de fecha no tiene un formato válido: dd/mm/yyyy"
                    }
                };
            }

            if(control.value.length === 10 &&
               !moment(control.value, "DD-MM-YYYY").isValid()) {
                return {
                    datenotvalid: {
                        value: control.value, 
                        format: "dd/mm/yyyy",
                        message: "La fecha capturada no es válida"
                    }
                };
            }
        }
        
        return null;
    };
}
