import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from "@angular/core";
import { FormControl, Validators } from "@angular/forms";
import Utils from "../../../shared/helpers/Utils";
import { fullLengthValidator } from "../..//validators/fullLengthValidator";
import { TextfieldMask } from "../../enums/TextfieldMask";
import { ICustomComponent } from "../../interfaces/ICustomComponent";
import { IEnabled } from "../../interfaces/IEnabled";
import { IRequired } from "../../interfaces/IRequired";
import { IVisible } from "../../interfaces/IVisible";
import { dateNotValidValidator } from "../../validators/dateNotValidValidator";
//import { ValidationContext, IsTrue } from "@angularlicious/rules-engine";
//import { ValidationContext } from "../../../shared/rules-engine/validation/ValidationContext";
//import { IsTrue } from "../../rules-engine/rules/IsTrue";
//import { ViewEncapsulation } from "@angular/core";

@Component({
  selector: "custom-textfield",
  templateUrl: "./custom-textfield.component.html",
  styleUrls: ["./custom-textfield.component.scss"]/*,
  encapsulation: ViewEncapsulation.None*/
})
export class CustomTextFieldComponent
  implements
  ICustomComponent,
  OnInit,
  IVisible,
  IEnabled,
  IRequired {
  //privadas
  type: string = "text";

  //public
  @Input() id: string;
  @Input() tag: string;
  @Input() model: string = "";
  @Output() modelChange = new EventEmitter<string>();
  @Input() minLength: number;
  @Input() maxLength: number;
  @Input() visible: boolean = true;
  @Input() readonly: boolean = false;
  @Input() enabled: boolean = true;
  @Input() required: boolean = false;
  //@Input() formControlName: string;

  @Input() fullLength: boolean = false;
  @Input() uppercase: boolean = false;
  @Input() enabledCopy: boolean = true;
  @Input() enabledCut: boolean = true;
  @Input() enabledPaste: boolean = true;
  @Input() enabledContextMenu: boolean = true;
  @Input() mask: TextfieldMask = TextfieldMask.None;
  @Input() regex: string;
  @Input() name: string = "";

  //@Input() visible: string = "true";

  @Input() metadata: any;

  //privadas
  pattern: string;
  min: string;
  max: string;

  control: FormControl = new FormControl("", []);



  @Output() click = new EventEmitter<any>();
  @Output() keyup = new EventEmitter<any>();
  @Output() keydown = new EventEmitter<any>();
  @Output() blur = new EventEmitter<any>();
  @Output() change = new EventEmitter<any>();
  

  enabledLog: boolean = false;

  //validationContext: ValidationContext = new ValidationContext();

  constructor() {
    //debugger

    //this.validationContext.withSource("dsfsd").addRule(new IsTrue('ThisIsTrue', 'This is not true', true, true));

    //this.validationContext.renderRules();


    //// Load the error/rule violations into the ServiceContext so that the information bubbles up to the caller of the service;
    //this.validationContext.results.forEach((e) => {
    //  debugger
    //  if (!e.isValid && e.rulePolicy.isDisplayable) {
    //    debugger
    //    //let serviceMessage = new ServiceMessage(e.rulePolicy.name, e.rulePolicy.message, MessageType.Error);
    //    //this.serviceContext.addMessage(serviceMessage);
    //  }
    //});
  }

  @ViewChild("element") element: ElementRef;

  private consoleLogEvent(event: string) {
    console.log(`${event} => CustomTextFieldComponent => ${Utils.IsValidStringNotWhiteSpace(this.id) ? this.id : ""}`);
  }

  getBoolean(value: any) {
    //debugger
    return Utils.StringToBoolean(value);
  }

  getNumber(value: any) {
    const result = Utils.StringToNumber(value);
    return Utils.IsValidNumber(result) ? result : 0;
  }

  ngOnInit() {
    //debugger
    if (this.enabledLog) {
      this.consoleLogEvent("ngOnInit");
      console.log("this.mask: " + this.mask);
    }
    
    if (this.mask === TextfieldMask.Email) {
      this.type = "email";
    }
    else if (this.mask === TextfieldMask.OnlyDigits) {
      this.type = "number";
      this.pattern = "[0-9]*";

      if (this.getNumber(this.maxLength) > 0) {
        const maxLength = this.getNumber(this.maxLength);

        this.min = "0";

        let sMaxLength = "";

        for (let index = 0; index < maxLength; index++) {
            sMaxLength = sMaxLength + "9";
        }

        this.max = sMaxLength;
      }
    }

    this.addValidators();
  }

  private addValidators() {
    //debugger
    const validators = [];

    if (this.getBoolean(this.required)) {
      validators.push(Validators.required);
    }

    if (this.mask === TextfieldMask.Date) {
      this.fullLength = true;
      this.maxLength = 10;
    }
    
    if (this.getBoolean(this.fullLength) && 
        this.getNumber(this.maxLength) > 0) {
      validators.push(fullLengthValidator(this.getNumber(this.maxLength)));
    }

    if (this.getNumber(this.minLength) > 0) {
      validators.push(Validators.minLength(this.getNumber(this.minLength)));
    }

    switch (this.mask) {
      case TextfieldMask.AlphaNumeric:
        validators.push(Validators.pattern(/^[ñáéíóúa-zÑÁÉÍÓÚA-Z0-9\-&'"\s,]*$/));
        break;
      /*case TextfieldMask.Currency:
        validators.push(Validators.pattern(/[0-9]{1,3}([\,][0-9]{3})*[\.][0-9]{2}/));
        break;*/
      case TextfieldMask.Date:
        //validators.push(Validators.pattern(/^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/));
        validators.push(dateNotValidValidator());
        break;
      case TextfieldMask.OnlyLetters:
        validators.push(Validators.pattern(/^[A-Za-z ñÑ]*$/));
        break;
      case TextfieldMask.Decimal:
        validators.push(Validators.pattern(/\d+(\.\d{1,3})?$/));
        break;
      case TextfieldMask.OnlyDigits:
        validators.push(Validators.pattern(/[0-9]*/));
        break;
      case TextfieldMask.Email:
        validators.push(Validators.email);
        break;
      case TextfieldMask.Regex:
        if(Utils.IsValidStringNotWhiteSpace(this.regex)) {
          validators.push(Validators.pattern(new RegExp(this.regex, "i")));
        }
        break;
    }

    if (validators.length > 0) {
      this.control = new FormControl("", validators);
    }
  }

  onClick(event: any) {
    if (this.enabledLog) {
      this.consoleLogEvent("onClick");
    }

    this.click.emit({component: this, event: event});
  }

  onKeyDown(event: any) {
    //debugger
    if (this.enabledLog) {
      this.consoleLogEvent("onKeyDown");
    }

    //const TABKEY = 9;
    const BACKSPACEKEY = 8;
    const valueChar = event.which || event.keyCode;

    if (valueChar == BACKSPACEKEY || valueChar === 37 || valueChar === 38 || valueChar === 39 || valueChar === 40) {
      return;
    }
    
    if (valueChar !== 13) {
      const element = event.target;

      if (this.mask === TextfieldMask.Date) {
        const length = element.value.length;
        let allowedCharacters = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];
        if ((length === 0 && !allowedCharacters.includes(event.key)) ||
            (length === 1 && !allowedCharacters.includes(event.key)) ||
            (length === 2 && event.key !== "/") ||
            (length === 3 && !allowedCharacters.includes(event.key)) ||
            (length === 4 && !allowedCharacters.includes(event.key)) ||
            (length === 5 && event.key !== "/") ||
            (length === 6 && !allowedCharacters.includes(event.key)) ||
            (length === 7 && !allowedCharacters.includes(event.key)) ||
            (length === 8 && !allowedCharacters.includes(event.key)) ||
            (length === 9 && !allowedCharacters.includes(event.key))) {// ||
            //length === 10) {
          event.preventDefault();
          //notification.setError(object, _valuesConfig.OnError, getMessage("fechaCaracterNoValido", "Carácter no válido para este campo de fecha"));
          return false;
        }
      }
      else if (this.mask === TextfieldMask.AlphaNumeric) {
        if (!(/^[ñáéíóúa-zÑÁÉÍÓÚA-Z0-9\-&'"\s,]*$/.test(event.key))) {
          event.preventDefault();
          //notification.setError(object, _valuesConfig.OnError, getMessage("soloLetrasCaracterNoValido", "Carácter no válido, solo se permiten letras"));
          return false;
        }
      }
      else if (this.mask === TextfieldMask.OnlyLetters) {
        if (!(/^[A-Za-z ñÑ]*$/.test(event.key))) {
          event.preventDefault();
          //notification.setError(object, _valuesConfig.OnError, getMessage("soloLetrasCaracterNoValido", "Carácter no válido, solo se permiten letras"));
          return false;
        }
      }
      else if (this.mask === TextfieldMask.Decimal) {
        console.log(event.key);
        let allowedCharacters = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "."];
        if (!allowedCharacters.includes(event.key)) {
          event.preventDefault();
          //notification.setError(object, _valuesConfig.OnError, getMessage("decimalCaracterNoValido", "Carácter no válido para este campo decimal"));
          return false;
        }
        const parts = event.srcElement.value.split(".");
        if (parts.length > 2) {
          event.preventDefault();
          //notification.setError(object, _valuesConfig.OnError, getMessage("decimalDoblePunto", "No se puede tener más de un punto decimal"));
          return false;
        }
        if (valueChar === 46) {
          if (!(parts.length === 1)) {
            event.preventDefault();
            //notification.setError(object, _valuesConfig.OnError, getMessage("decimalDoblePunto", "No se puede tener más de un punto decimal"));
            return false;
          }
        }
        if (parts[0].length >= 14) {
          event.preventDefault();
          //notification.setError(object, _valuesConfig.OnError, getMessage("decimalLogitudNoValida", "Longitud no válida para este campo decimal"));
          return false;
        }
        if (parts.length === 2 && parts[1].length >= 2) {
          event.preventDefault();
          //notification.setError(object, _valuesConfig.OnError, getMessage("decimalLogitudNoValidaDecimal", "La longitud es no válida en la parte decimal de este número"));
          return false;
        }

        const character = event.key;
        const sNewValue = `${element.value}${character}`;
        const newValue = Number(sNewValue);
        if (isNaN(newValue) || Utils.HasDecimalPlace(sNewValue, 3)) {
          event.preventDefault();
          //notification.setError(object, _valuesConfig.OnError, getMessage("decimalNoValido", "Este valor decimal no es válido"));
          return false;
        }
      }
      else if (this.mask === TextfieldMask.OnlyDigits) {
        if (!(valueChar >= 48 && valueChar <= 57 || valueChar >= 96 && valueChar <= 105)) {
          event.preventDefault();
          //notification.setError(object, _valuesConfig.OnError, getMessage("soloDigitosCaracterNoValido", "Carácter no válido para este campo de solo digitos"));
          return false;
        }
      }

      //notification.setCorrecto(object, _valuesConfig.OnCorrecto);

      this.keydown.emit({component: this, event: event});
    }
  }
  
  onKeyUp(event: any) {
    //debugger
    if (this.enabledLog) {
      this.consoleLogEvent("onKeyUp");
    }

    this.keyup.emit({component: this, event: event});
  }
  
  onBlur(event: any) {
    //debugger
    if (this.enabledLog) {
      this.consoleLogEvent("onBlur");
    }

    this.blur.emit({component: this, event: event});
  }

  onInput(event: any) {
    //debugger
    if (this.enabledLog) {
      this.consoleLogEvent("onInput");
      console.log("this.uppercase: " + this.uppercase);
      console.log("this.mask: " + this.mask);
    }

    this.onChangeInput(event);
  }

  onChange(event: any) {
    //debugger
    if (this.enabledLog) {
      this.consoleLogEvent("onChange");
    }

    this.onChangeInput(event);
  }

  private onChangeInput(event: any) {
    if (this.getBoolean(this.uppercase)) {
      const newValue = event.target.value.toUpperCase();
      this.control.setValue(newValue);
    }

    const maxLength = this.getNumber(this.maxLength);

    if (maxLength > 0 && event.target.value.length > maxLength) {
      const newValue = event.target.value.slice(0, maxLength);
      this.control.setValue(newValue);
    }

    this.change.emit({component: this, event: event});
  }

  onCopy(event: any) {
    //debugger
    if (this.enabledLog) {
      this.consoleLogEvent("onCopy");
      console.log("this.enabledCopy: " + this.enabledCopy);
    }

    if (!this.getBoolean(this.enabledCopy)) {
      event.preventDefault();
    }
  }

  onCut(event: any) {
    //debugger
    if (this.enabledLog) {
      this.consoleLogEvent("onCut");
      console.log("this.enabledCut: " + this.enabledCut);
    }

    if (!this.getBoolean(this.enabledCut)) {
      event.preventDefault();
    }
  }

  onPaste(event: any) {
    //debugger
    if (this.enabledLog) {
      this.consoleLogEvent("onPaste");
      console.log("this.enabledPaste: " + this.enabledPaste);
    }

    if (!this.getBoolean(this.enabledPaste)) {
      event.preventDefault();
    }
  }

  onContextMenu(event: any) {
    //debugger
    if (this.enabledLog) {
      this.consoleLogEvent("onContextMenu");
      console.log("this.enabledContextMenu: " + this.enabledContextMenu);
    }

    if (!this.getBoolean(this.enabledContextMenu)) {
      event.preventDefault();
    }
  }

  getErrorMessage() {
    //let result = "";

    if (this.control.hasError("required")) {
      return "Ningún valor ingresado en un campo obligatorio";
    }

    if (this.control.hasError("minlength")) {
      return `La longitud mínima requerida para este campo es de ${this.minLength} caracteres`;
    }

    if (this.control.hasError("fulllength")) {
      return `La longitud válida para este campo es de ${this.maxLength} caracteres`;
    }
    
    if (this.control.hasError("pattern")) {
      switch (this.mask) {
        case TextfieldMask.AlphaNumeric:
          return "Este campo alfanumérico no tiene un formato válido";
        /*case TextfieldMask.Currency:
          return "Este campo de moneda no tiene un formato válido";*/
        /*case TextfieldMask.Date:
          return "Este campo de fecha no tiene un formato válido: dd/mm/yyyy";*/
        case TextfieldMask.OnlyLetters:
          return "Este campo no tiene un formato válido";
        case TextfieldMask.Decimal:
          return "Este valor decimal no es válido";
        case TextfieldMask.OnlyDigits:
          return "Solo se permiten números en este campo";
        case TextfieldMask.Regex:
          return "Este campo no tiene un formato válido";
      }
    }

    if(this.control.hasError("datenotvalid")) {
      return this.control.errors.datenotvalid.message;
    }

    if (this.control.hasError("email")) {
      return "Este correo electrónico no tiene un formato válido";
    }
    
    return "";
  }

  getType(): string {
    return "CustomTextFieldComponent";
  }

  set value(value: string) {
    this.model = value;
    this.modelChange.emit(this.model);
    this.change.emit({ component: this, event: undefined });
  }

  get value(): string {
    return this.model;
  }
}
