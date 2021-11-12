import { Directive, Input } from "@angular/core";
import { ICustomComponent } from "../interfaces/ICustomComponent";

@Directive({
  selector: "[custom-component-validate]"
})
export class CustomComponentValidateDirective {
  private _customComponentFormControl: ICustomComponent;

  @Input("custom-component-validate")
  set component(value: ICustomComponent) {
    this._customComponentFormControl = value;
  }
  get component(): ICustomComponent {
    return this._customComponentFormControl;
  }
}
