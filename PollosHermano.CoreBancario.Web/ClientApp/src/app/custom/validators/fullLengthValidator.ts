import { ValidationErrors, ValidatorFn, AbstractControl } from "@angular/forms";
import Utils from "../../shared/helpers/Utils";

export function fullLengthValidator(maxLength: number): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        if (!Utils.IsValidStringNotWhiteSpace(control.value) || !(maxLength > 0)) {
            return null;
        }
    
        const length = control.value.length;
        
        return length === maxLength ? null : {fulllength: {length, maxLength}};
    };
}
