import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
import { NgbTimeStruct } from "@ng-bootstrap/ng-bootstrap";
import { CustomTimeComponent } from "../components/custom-time/custom-time.component";

export function isValidTimeValidator(customTimeComponent: CustomTimeComponent): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const model = customTimeComponent.model as NgbTimeStruct;

    if (model === undefined || model === null) {
      return null;
    }
    else if ((model.hour === undefined || model.hour === null) || (model.minute === undefined || model.minute === null) || (model.second === undefined || model.second === null)) {
      return {
        isValidTimeValidator: {
          component: customTimeComponent
        }
      };
    }

    return null;
  };
}
