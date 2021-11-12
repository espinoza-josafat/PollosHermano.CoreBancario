import {
    AfterContentChecked,
    AfterContentInit,
    AfterViewChecked,
    AfterViewInit,

    DoCheck,
    Injectable,
    OnChanges,
    OnDestroy,
    OnInit,
    QueryList,
    SimpleChanges
} from "@angular/core";
import { CustomComponentValidateDirective } from "../../directives/custom-component-validate.directive";
import Utils from "../../../shared/helpers/Utils";
import { CustomCheckComponent } from "../custom-check/custom-check.component";
import { CustomComboComponent } from "../custom-combo/custom-combo.component";
import { CustomHiddenComponent } from "../custom-hidden/custom-hidden.component";
import { CustomRadioComponent } from "../custom-radio/custom-radio.component";
import { CustomTextAreaComponent } from "../custom-textarea/custom-textarea.component";
import { CustomTextFieldComponent } from "../custom-textfield/custom-textfield.component";
import { CustomDateComponent } from "../custom-date/custom-date.component";
import { TextfieldMask } from "../../enums/TextfieldMask";

@Injectable()
export abstract class CustomBaseComponent
  implements
  OnChanges,
  OnInit,
  DoCheck,
  AfterContentInit,
  AfterContentChecked,
  AfterViewInit,
  AfterViewChecked,
  OnDestroy
{

  ngOnChanges(changes: SimpleChanges) {

  }

  ngOnInit() {

  }

  ngDoCheck() {

  }

  ngAfterContentInit() {

  }

  ngAfterContentChecked() {

  }

  ngAfterViewInit() {

  }

  ngAfterViewChecked() {

  }

  ngOnDestroy() {

  }

  generateModel(validators: QueryList<CustomComponentValidateDirective>) {
    const model: any = {};

    if (validators === undefined || validators === null || validators.length === 0) {
      return model;
    }

    validators.forEach((x: CustomComponentValidateDirective) => {
      const type = x.component.getType();
      let name = x.component.name;

      if (!Utils.IsValidStringNotWhiteSpace(type) ||
        !Utils.IsValidStringNotWhiteSpace(name)) {
        return;
      }

      name = Utils.FirstCharToLowerCase(name);

      switch (type) {
        case "CustomTextFieldComponent":
          const customTextFieldComponent = (x.component as CustomTextFieldComponent);
          const valueCustomTextFieldComponent = customTextFieldComponent.value;
          if (valueCustomTextFieldComponent !== "") {
            if (customTextFieldComponent.mask === TextfieldMask.Decimal || customTextFieldComponent.mask === TextfieldMask.OnlyDigits) {
              model[name] = Number(valueCustomTextFieldComponent);
            }
            else {
              model[name] = valueCustomTextFieldComponent;
            }
          }
          break;
        case "CustomHiddenComponent":
          const customHiddenComponent = (x.component as CustomHiddenComponent);
          const valueCustomHiddenComponent = customHiddenComponent.value;
          if (valueCustomHiddenComponent !== "") {
            model[name] = valueCustomHiddenComponent;
          }
          break;
        case "CustomTextAreaComponent":
          const customTextAreaComponent = (x.component as CustomTextAreaComponent);
          const valueCustomTextAreaComponent = customTextAreaComponent.value;
          if (valueCustomTextAreaComponent !== "") {
            model[name] = valueCustomTextAreaComponent;
          }
          break;
        case "CustomCheckComponent":
          const customCheckComponent = (x.component as CustomCheckComponent);
          const valueCustomCheckComponent = customCheckComponent.value;
          model[name] = valueCustomCheckComponent;
          break;
        case "CustomComboComponent":
          const customComboComponent = (x.component as CustomComboComponent);
          const valueCustomComboComponent = customComboComponent.value;
          if (valueCustomComboComponent !== undefined &&
              valueCustomComboComponent !== null) {
            model[name] = valueCustomComboComponent;
          }
          break;
        case "CustomRadioComponent":
          const customRadioComponent = (x.component as CustomRadioComponent);
          const valueCustomRadioComponent = customRadioComponent.value;
          if (valueCustomRadioComponent !== undefined &&
              valueCustomRadioComponent !== null) {
            model[name] = valueCustomRadioComponent;
          }
          break;
        case "CustomDateComponent":
          const customDateComponent = (x.component as CustomDateComponent);
          const valueCustomDateComponent = customDateComponent.value;
          if (valueCustomDateComponent !== undefined &&
              valueCustomDateComponent !== null) {
            model[name] = valueCustomDateComponent;
          }
          break;
      }
    });

    return model;
  }

  populateModel(model: any, validators: QueryList<CustomComponentValidateDirective>) {
    if (!Utils.IsValidJsonObject(model) ||
        validators === undefined ||
        validators === null ||
        validators.length === 0) {
      return;
    }

    const keys = Object.keys(model);

    keys.forEach((key: string) => this.setValue(key, model[key], validators));
  }

  setValue(key: string, value: any, validators: QueryList<CustomComponentValidateDirective>) {
    const validator = validators.find((x: CustomComponentValidateDirective) => Utils.IsValidStringNotEmpty(x.component.name) && Utils.FirstCharToLowerCase(x.component.name) === Utils.FirstCharToLowerCase(key));

    if (!validator) {
      return;
    }

    const type = validator.component.getType();

    if (!Utils.IsValidStringNotWhiteSpace(type)) {
      return;
    }

    switch (type) {
      case "CustomTextFieldComponent":
        const customTextFieldComponent = (validator.component as CustomTextFieldComponent);
        if (value !== undefined && value !== null) {
          customTextFieldComponent.value = value;
        }
        break;
      case "CustomHiddenComponent":
        const customHiddenComponent = (validator.component as CustomHiddenComponent);
        if (value !== undefined && value !== null) {
          customHiddenComponent.value = value;
        }
        break;
      case "CustomTextAreaComponent":
        const customTextAreaComponent = (validator.component as CustomTextAreaComponent);
        if (value !== undefined && value !== null) {
          customTextAreaComponent.value = value;
        }
        break;
      case "CustomCheckComponent":
        const customCheckComponent = (validator.component as CustomCheckComponent);
        if (Utils.IsValidBoolean(value)) {
          customCheckComponent.value = value;
        }
        break;
      case "CustomComboComponent":
        const customComboComponent = (validator.component as CustomComboComponent);
        if (value !== undefined && value !== null) {
          customComboComponent.value = value;
        }
        break;
      case "CustomRadioComponent":
        const customRadioComponent = (validator.component as CustomRadioComponent);
        if (value !== undefined && value !== null) {
          customRadioComponent.value = value;
        }
        break;
      case "CustomDateComponent":
        const customDateComponent = (validator.component as CustomDateComponent);
        if (value !== undefined && value !== null) {
          customDateComponent.value = value;
        }
        break;
    }
  }

  setDataSource(key: string, dataSource: any, validators: QueryList<CustomComponentValidateDirective>) {
    const validator = validators.find((x: CustomComponentValidateDirective) => Utils.IsValidStringNotEmpty(x.component.name) && Utils.FirstCharToLowerCase(x.component.name) === Utils.FirstCharToLowerCase(key));

    if (!validator) {
      return;
    }

    const type = validator.component.getType();

    if (!Utils.IsValidStringNotWhiteSpace(type)) {
      return;
    }

    switch (type) {
      case "CustomComboComponent":
        const customComboComponent = (validator.component as CustomComboComponent);
        if (dataSource !== undefined && dataSource !== null) {
          customComboComponent.dataSource = dataSource;
        }
        break;
      case "CustomRadioComponent":
        const customRadioComponent = (validator.component as CustomRadioComponent);
        if (dataSource !== undefined && dataSource !== null) {
          customRadioComponent.dataSource = dataSource;
        }
        break;
    }
  }
}
