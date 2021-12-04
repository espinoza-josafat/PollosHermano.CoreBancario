import { ChangeDetectorRef, Component, ElementRef, EventEmitter, Injectable, Injector, Input, OnInit, Output, ViewChild } from "@angular/core";
import { FormControl, NgControl, Validators } from "@angular/forms";
import Utils from "../../../shared/helpers/Utils";
//import { fullLengthValidator } from "../../validators/fullLengthValidator";
//import { TextfieldMask } from "../../enums/TextfieldMask";
import { ICustomComponent } from "../../interfaces/ICustomComponent";
import { IEnabled } from "../../interfaces/IEnabled";
import { IRequired } from "../../interfaces/IRequired";
import { IVisible } from "../../interfaces/IVisible";
//import { dateNotValidValidator } from "../../validators/dateNotValidValidator";
//import { ValidationContext, IsTrue } from "@angularlicious/rules-engine";
//import { ValidationContext } from "../../../shared/rules-engine/validation/ValidationContext";
//import { IsTrue } from "../../rules-engine/rules/IsTrue";
//import { ViewEncapsulation } from "@angular/core";

import { NgbDateStruct, NgbDatepickerI18n, NgbCalendar, NgbDateParserFormatter, NgbPopover, NgbPopoverConfig, NgbTimeStruct, NgbDatepicker } from '@ng-bootstrap/ng-bootstrap';
import { DateTimeModel } from "./DateTimeModel";
import { noop } from "rxjs";

const I18N_VALUES = {
  en: {
    weekdays: ['Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa', 'Su'],
    months: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
  },
  es: {
    weekdays: ['Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá', 'Do'],
    months: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
  }
};

// Define a service holding the language. You probably already have one if your app is i18ned.
@Injectable()
export class I18n {
  language = 'es';
}

// Define custom service providing the months and weekdays translations
@Injectable()
export class CustomDatepickerI18n extends NgbDatepickerI18n {
  constructor(private _i18n: I18n) {
    super();
  }

  getWeekdayShortName(weekday: number): string {
    return I18N_VALUES[this._i18n.language].weekdays[weekday - 1];
  }
  getMonthShortName(month: number): string {
    return I18N_VALUES[this._i18n.language].months[month - 1];
  }
  getMonthFullName(month: number): string {
    return this.getMonthShortName(month);
  }
  getDayAriaLabel(date: NgbDateStruct): string {
    return `${date.day}-${date.month}-${date.year}`;
  }
}

@Injectable()
export class NgbDateCustomParserFormatter extends NgbDateParserFormatter {
  parse(value: string): NgbDateStruct {
    if (value) {
      const dateParts = value.trim().split("/");
      if (dateParts.length === 1 && isNumber(dateParts[0])) {
        return { day: toInteger(dateParts[0]), month: null, year: null };
      } else if (dateParts.length === 2 && isNumber(dateParts[0]) && isNumber(dateParts[1])) {
        return {
          day: toInteger(dateParts[0]),
          month: toInteger(dateParts[1]),
          year: null
        };
      } else if (dateParts.length === 3 && isNumber(dateParts[0]) && isNumber(dateParts[1]) && isNumber(dateParts[2])) {
        return {
          day: toInteger(dateParts[0]),
          month: toInteger(dateParts[1]),
          year: toInteger(dateParts[2])
        };
      }
    }
    return null;
  }

  format(date: NgbDateStruct): string {
    return date
      ? `${isNumber(date.day) ? padNumber(date.day) : ""}/${isNumber(date.month) ? padNumber(date.month) : ""}/${date.year}`
      : "";
  }
}
export function toInteger(value: any): number {
  return parseInt(`${value}`, 10);
}

export function isNumber(value: any): value is number {
  return !isNaN(toInteger(value));
}

export function padNumber(value: number) {
  if (isNumber(value)) {
    return `0${value}`.slice(-2);
  } else {
    return "";
  }
}

@Component({
  selector: "custom-datetime",
  templateUrl: "./custom-datetime.component.html",
  styleUrls: ["./custom-datetime.component.scss"],
  providers: [
    { provide: NgbDateParserFormatter, useClass: NgbDateCustomParserFormatter },
    I18n,
    { provide: NgbDatepickerI18n, useClass: CustomDatepickerI18n },
  ]
})
export class CustomDateTimeComponent
  implements
  ICustomComponent,
  OnInit,
  IVisible,
  IEnabled,
  IRequired {
  //privadas
  underlyingElement: any;

  @Input()
  dateString: string;

  //public
  @Input() id: string;
  @Input() tag: string;

  get model(): DateTimeModel {
    return this.datetime;
  }
  @Input() set model(value: DateTimeModel) {
    this.datetime = value;
  }
  @Output() modelChange = new EventEmitter<any>();
  @Input()
  //inputDatetimeFormat = "yyyy/MM/dd HH:mm:ss";
  //@Input() minLength: number;
  //@Input() maxLength: number;
  @Input() visible: boolean = true;
  @Input() readonly: boolean = true;
  @Input() enabled: boolean = true;
  @Input() required: boolean = false;
  //@Input() formControlName: string;

  //@Input() fullLength: boolean = false;
  //@Input() uppercase: boolean = false;
  @Input() enabledCopy: boolean = false;
  @Input() enabledCut: boolean = false;
  @Input() enabledPaste: boolean = false;
  @Input() enabledContextMenu: boolean = false;
  //@Input() mask: TextfieldMask = TextfieldMask.None;
  //@Input() regex: string;
  @Input() name: string = "";

  @Input() minValue: any = { year: 1920, month: 1, day: 1 };
  @Input() maxValue: any = { year: new Date().getFullYear(), month: new Date().getMonth() + 1, day: new Date().getDate() };

  @Input()
  seconds = true;

  //@Input() visible: string = "true";

  @Input() metadata: any;

  //privadas
  //pattern: string;
  //min: string;
  //max: string;

  control: FormControl = new FormControl("", []);



  @Output() click = new EventEmitter<any>();
  @Output() keyup = new EventEmitter<any>();
  @Output() keydown = new EventEmitter<any>();
  @Output() blur = new EventEmitter<any>();
  @Output() change = new EventEmitter<any>();
  

  enabledLog: boolean = false;

  //validationContext: ValidationContext = new ValidationContext();


  private showTimePickerToggle = false;

  private datetime: DateTimeModel = new DateTimeModel();
  private firstTimeAssign = true;

  // @ViewChild(NgbDatepicker, { static: true })
  // private dp: NgbDatepicker;

  @ViewChild(NgbPopover, { static: true })
  private popover: NgbPopover;

  private onTouched: () => void = noop;
  private onComponentChange: (_: any) => void = noop;

  //private ngControl: NgControl;


  constructor(private _i18n: I18n, private config: NgbPopoverConfig, private inj: Injector, private cdRef: ChangeDetectorRef) {
    //debugger
    config.autoClose = "outside";
    config.placement = "auto";

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
    console.log(`${event} => CustomDateTimeComponent => ${Utils.IsValidStringNotWhiteSpace(this.id) ? this.id : ""}`);
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
      //console.log("this.mask: " + this.mask);
    }

    //this.ngControl = this.inj.get(NgControl);

    //if (this.mask === TextfieldMask.Email) {
    //  this.type = "email";
    //}
    //else if (this.mask === TextfieldMask.OnlyDigits) {
    //  this.type = "number";
    //  this.pattern = "[0-9]*";

    //  if (this.getNumber(this.maxLength) > 0) {
    //    const maxLength = this.getNumber(this.maxLength);

    //    this.min = "0";

    //    let sMaxLength = "";

    //    for (let index = 0; index < maxLength; index++) {
    //        sMaxLength = sMaxLength + "9";
    //    }

    //    this.max = sMaxLength;
    //  }
    //}

    this.addValidators();
  }

  private addValidators() {
    //debugger
    const validators = [];

    if (this.getBoolean(this.required)) {
      validators.push(Validators.required);
    }

    //if (this.mask === TextfieldMask.Date) {
    //  this.fullLength = true;
    //  this.maxLength = 10;
    //}
    
    //if (this.getBoolean(this.fullLength) && 
    //    this.getNumber(this.maxLength) > 0) {
    //  validators.push(fullLengthValidator(this.getNumber(this.maxLength)));
    //}

    //if (this.getNumber(this.minLength) > 0) {
    //  validators.push(Validators.minLength(this.getNumber(this.minLength)));
    //}

    //switch (this.mask) {
    //  case TextfieldMask.AlphaNumeric:
    //    validators.push(Validators.pattern(/^[ñáéíóúa-zÑÁÉÍÓÚA-Z0-9\-&'"\s,]*$/));
    //    break;
    //  /*case TextfieldMask.Currency:
    //    validators.push(Validators.pattern(/[0-9]{1,3}([\,][0-9]{3})*[\.][0-9]{2}/));
    //    break;*/
    //  case TextfieldMask.Date:
    //    //validators.push(Validators.pattern(/^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/));
    //    validators.push(dateNotValidValidator());
    //    break;
    //  case TextfieldMask.OnlyLetters:
    //    validators.push(Validators.pattern(/^[A-Za-z ñÑ]*$/));
    //    break;
    //  case TextfieldMask.Decimal:
    //    validators.push(Validators.pattern(/\d+(\.\d{1,3})?$/));
    //    break;
    //  case TextfieldMask.OnlyDigits:
    //    validators.push(Validators.pattern(/[0-9]*/));
    //    break;
    //  case TextfieldMask.Email:
    //    validators.push(Validators.email);
    //    break;
    //  case TextfieldMask.Regex:
    //    if(Utils.IsValidStringNotWhiteSpace(this.regex)) {
    //      validators.push(Validators.pattern(new RegExp(this.regex, "i")));
    //    }
    //    break;
    //}

    if (validators.length > 0) {
      this.control = new FormControl("", validators);
    }
  }

  ngAfterViewInit(): void {
    this.popover.hidden.subscribe($event => {
      this.showTimePickerToggle = false;
    });
  }

  ngAfterContentChecked(): void {
    this.cdRef.detectChanges();
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

      //if (this.mask === TextfieldMask.Date) {
      //  const length = element.value.length;
      //  let allowedCharacters = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];
      //  if ((length === 0 && !allowedCharacters.includes(event.key)) ||
      //      (length === 1 && !allowedCharacters.includes(event.key)) ||
      //      (length === 2 && event.key !== "/") ||
      //      (length === 3 && !allowedCharacters.includes(event.key)) ||
      //      (length === 4 && !allowedCharacters.includes(event.key)) ||
      //      (length === 5 && event.key !== "/") ||
      //      (length === 6 && !allowedCharacters.includes(event.key)) ||
      //      (length === 7 && !allowedCharacters.includes(event.key)) ||
      //      (length === 8 && !allowedCharacters.includes(event.key)) ||
      //      (length === 9 && !allowedCharacters.includes(event.key))) {// ||
      //      //length === 10) {
      //    event.preventDefault();
      //    //notification.setError(object, _valuesConfig.OnError, getMessage("fechaCaracterNoValido", "Carácter no válido para este campo de fecha"));
      //    return false;
      //  }
      //}
      //else if (this.mask === TextfieldMask.AlphaNumeric) {
      //  if (!(/^[ñáéíóúa-zÑÁÉÍÓÚA-Z0-9\-&'"\s,]*$/.test(event.key))) {
      //    event.preventDefault();
      //    //notification.setError(object, _valuesConfig.OnError, getMessage("soloLetrasCaracterNoValido", "Carácter no válido, solo se permiten letras"));
      //    return false;
      //  }
      //}
      //else if (this.mask === TextfieldMask.OnlyLetters) {
      //  if (!(/^[A-Za-z ñÑ]*$/.test(event.key))) {
      //    event.preventDefault();
      //    //notification.setError(object, _valuesConfig.OnError, getMessage("soloLetrasCaracterNoValido", "Carácter no válido, solo se permiten letras"));
      //    return false;
      //  }
      //}
      //else if (this.mask === TextfieldMask.Decimal) {
      //  console.log(event.key);
      //  let allowedCharacters = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "."];
      //  if (!allowedCharacters.includes(event.key)) {
      //    event.preventDefault();
      //    //notification.setError(object, _valuesConfig.OnError, getMessage("decimalCaracterNoValido", "Carácter no válido para este campo decimal"));
      //    return false;
      //  }
      //  const parts = event.srcElement.value.split(".");
      //  if (parts.length > 2) {
      //    event.preventDefault();
      //    //notification.setError(object, _valuesConfig.OnError, getMessage("decimalDoblePunto", "No se puede tener más de un punto decimal"));
      //    return false;
      //  }
      //  if (valueChar === 46) {
      //    if (!(parts.length === 1)) {
      //      event.preventDefault();
      //      //notification.setError(object, _valuesConfig.OnError, getMessage("decimalDoblePunto", "No se puede tener más de un punto decimal"));
      //      return false;
      //    }
      //  }
      //  if (parts[0].length >= 14) {
      //    event.preventDefault();
      //    //notification.setError(object, _valuesConfig.OnError, getMessage("decimalLogitudNoValida", "Longitud no válida para este campo decimal"));
      //    return false;
      //  }
      //  if (parts.length === 2 && parts[1].length >= 2) {
      //    event.preventDefault();
      //    //notification.setError(object, _valuesConfig.OnError, getMessage("decimalLogitudNoValidaDecimal", "La longitud es no válida en la parte decimal de este número"));
      //    return false;
      //  }

      //  const character = event.key;
      //  const sNewValue = `${element.value}${character}`;
      //  const newValue = Number(sNewValue);
      //  if (isNaN(newValue) || Utils.HasDecimalPlace(sNewValue, 3)) {
      //    event.preventDefault();
      //    //notification.setError(object, _valuesConfig.OnError, getMessage("decimalNoValido", "Este valor decimal no es válido"));
      //    return false;
      //  }
      //}
      //else if (this.mask === TextfieldMask.OnlyDigits) {
      //  if (!(valueChar >= 48 && valueChar <= 57 || valueChar >= 96 && valueChar <= 105)) {
      //    event.preventDefault();
      //    //notification.setError(object, _valuesConfig.OnError, getMessage("soloDigitosCaracterNoValido", "Carácter no válido para este campo de solo digitos"));
      //    return false;
      //  }
      //}

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

    this.inputBlur(event);

    this.blur.emit({component: this, event: event});
  }

  onInput(event: any) {
    //debugger
    if (this.enabledLog) {
      this.consoleLogEvent("onInput");
      //console.log("this.uppercase: " + this.uppercase);
      //console.log("this.mask: " + this.mask);
    }

    this.onChangeInput(event);
  }

  onChange(event: any) {
    //debugger
    if (this.enabledLog) {
      this.consoleLogEvent("onChange");
    }

    this.onInputChange(event);

    this.onChangeInput(event);
  }

  private onChangeInput(event: any) {
    //if (this.getBoolean(this.uppercase)) {
    //  const newValue = event.target.value.toUpperCase();
    //  this.control.setValue(newValue);
    //}

    //const maxLength = this.getNumber(this.maxLength);

    //if (maxLength > 0 && event.target.value.length > maxLength) {
    //  const newValue = event.target.value.slice(0, maxLength);
    //  this.control.setValue(newValue);
    //}

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

  writeValue(newModel: string) {
    if (newModel) {
      this.datetime = Object.assign(
        this.datetime,
        DateTimeModel.fromLocalString(newModel)
      );
      this.dateString = newModel;
      this.setDateStringModel();
    } else {
      this.datetime = new DateTimeModel();
    }
  }

  registerOnChange(fn: any): void {
    this.onComponentChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  toggleDateTimeState($event) {
    this.showTimePickerToggle = !this.showTimePickerToggle;
    $event.stopPropagation();
  }

  setDisabledState?(isDisabled: boolean): void {
    this.enabled = !isDisabled;
  }

  private onInputChange($event: any) {
    const value = $event.target.value;
    const dt = DateTimeModel.fromLocalString(value);

    if (dt) {
      this.datetime = dt;
      this.setDateStringModel();
    } else if (value.trim() === "") {
      this.clear();
    } else {
      this.onComponentChange(value);
    }
  }

  onDateChange($event: string | NgbDateStruct, dp: NgbDatepicker) {
    const date = new DateTimeModel($event);

    if (!date) {
      this.dateString = this.dateString;
      return;
    }

    if (!this.datetime) {
      this.datetime = date;
    }

    this.datetime.year = date.year;
    this.datetime.month = date.month;
    this.datetime.day = date.day;

    /*const adjustedDate = new Date(this.datetime.toString());
    if (this.datetime.timeZoneOffset !== adjustedDate.getTimezoneOffset()) {
      this.datetime.timeZoneOffset = adjustedDate.getTimezoneOffset();
    }*/

    this.setDateStringModel();
  }

  onTimeChange(event: NgbTimeStruct) {
    this.datetime.hour = event.hour;
    this.datetime.minute = event.minute;
    this.datetime.second = event.second;

    this.setDateStringModel();
  }

  setDateStringModel() {
    this.dateString = this.datetime.toString();

    if (!this.firstTimeAssign) {
      this.onComponentChange(this.dateString);
    } else {
      // Skip very first assignment to null done by Angular
      if (this.dateString !== null) {
        this.firstTimeAssign = false;
      }
    }

    this.modelChangeEmit();
  }

  inputBlur($event) {
    this.onTouched();
  }

  public clear(): void {
    this.datetime = new DateTimeModel();
    this.dateString = "";
    this.onComponentChange(this.dateString);
    this.modelChangeEmit();
    //this.cdRef.detectChanges();
  }

  private modelChangeEmit() {
    this.modelChange.emit(this.model);
    this.change.emit({ component: this, event: undefined });
  }

  getErrorMessage() {
    //debugger
    //let result = "";

    if (this.control.hasError("required")) {
      return "Ningún valor ingresado en un campo obligatorio";
    }

    //if (this.control.hasError("minlength")) {
    //  return `La longitud mínima requerida para este campo es de ${this.minLength} caracteres`;
    //}

    //if (this.control.hasError("fulllength")) {
    //  return `La longitud válida para este campo es de ${this.maxLength} caracteres`;
    //}
    
    //if (this.control.hasError("pattern")) {
    //  switch (this.mask) {
    //    case TextfieldMask.AlphaNumeric:
    //      return "Este campo alfanumérico no tiene un formato válido";
    //    /*case TextfieldMask.Currency:
    //      return "Este campo de moneda no tiene un formato válido";*/
    //    /*case TextfieldMask.Date:
    //      return "Este campo de fecha no tiene un formato válido: dd/mm/yyyy";*/
    //    case TextfieldMask.OnlyLetters:
    //      return "Este campo no tiene un formato válido";
    //    case TextfieldMask.Decimal:
    //      return "Este valor decimal no es válido";
    //    case TextfieldMask.OnlyDigits:
    //      return "Solo se permiten números en este campo";
    //    case TextfieldMask.Regex:
    //      return "Este campo no tiene un formato válido";
    //  }
    //}

    //if(this.control.hasError("datenotvalid")) {
    //  return this.control.errors.datenotvalid.message;
    //}

    //if (this.control.hasError("email")) {
    //  return "Este correo electrónico no tiene un formato válido";
    //}
    
    return "";
  }

  getType(): string {
    return "CustomDateTimeComponent";
  }

  set value(value: string) {
    try {
      let items = [];
      if (value.includes("T") || value.includes(" ")) {
        if (value.includes("T")) {
          items = value.split("T");
        }
        else if (value.includes(" ")) {
          items = value.split(" ");
        }
      }
      let time = "00:00:00";

      if (items.length > 0) {
        value = items[0];
      }
      if (items.length > 1) {
        time = items[1];
      }

      if (time.includes(".")) {
        time = time.split(".")[0];
      }

      let dataDate = [];
      if (value.includes("-")) {
        dataDate = value.split("-");
      }
      else if (value.includes("/")) {
        dataDate = value.split("/");
      }

      let dataTime = [];
      if (time.includes(":")) {
        dataTime = time.split(":");
      }

      if (dataDate && dataDate.length === 3) {
        this.datetime = new DateTimeModel({
          year: parseInt(dataDate[0]),
          month: parseInt(dataDate[1]),
          day: parseInt(dataDate[2]),
          hour: parseInt(dataTime[0]),
          minute: parseInt(dataTime[1]),
          second: parseInt(dataTime[2])
        });
        this.setDateStringModel();
      }
    }
    catch (error) {
      console.log(error);
    }
  }

  get value(): string {
    return this.model !== undefined && this.model !== null ? this.datetime.toString() : null;
  }
}
