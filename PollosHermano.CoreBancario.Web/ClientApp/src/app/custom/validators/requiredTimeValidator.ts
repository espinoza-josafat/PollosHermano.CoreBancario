import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
import { NgbTimeStruct } from "@ng-bootstrap/ng-bootstrap";
import { CustomTimeComponent } from "../components/custom-time/custom-time.component";

export function requiredTimeValidator(customTimeComponent: CustomTimeComponent): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const model = customTimeComponent.model as NgbTimeStruct;

    if (model === undefined || model === null) {
      return {
        requiredTimeValidator: {
          component: customTimeComponent
        }
      };
    }

    return null;
  };
}
