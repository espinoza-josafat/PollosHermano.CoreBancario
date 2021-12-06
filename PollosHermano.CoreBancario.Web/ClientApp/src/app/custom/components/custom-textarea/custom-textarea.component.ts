import { ChangeDetectorRef, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from "@angular/core";
//import { TextfieldMask } from "../../enums/TextfieldMask";
import { FormControl, Validators } from "@angular/forms";
import Utils from "../../../shared/helpers/Utils";
import { ICustomComponent } from "../../interfaces/ICustomComponent";
import { IEnabled } from "../../interfaces/IEnabled";
import { IRequired } from "../../interfaces/IRequired";
import { IVisible } from "../../interfaces/IVisible";
//import { fullLengthValidator } from "../../validators/fullLengthValidator";

@Component({
  selector: "custom-textarea",
  templateUrl: "./custom-textarea.component.html",
  styleUrls: ["./custom-textarea.component.scss"]
})
export class CustomTextAreaComponent
  implements
  ICustomComponent,
  OnInit,
  IVisible,
  IEnabled,
  IRequired {

  //privadas
  //type: string = "text";

  //public
  @Input() id: string;
  @Input() tag: string;
  @Input() model: string = "";
  @Output() modelChange = new EventEmitter<string>();
  @Input() minLength: number;
  @Input() maxLength: number;
  @Input() cols: number;
  @Input() rows: number;
  @Input() visible: boolean = true;
  @Input() readonly: boolean = false;
  @Input() enabled: boolean = true;
  @Input() required: boolean = false;
  @Input() name: string = "";

  //@Input() fullLength: string = "false";
  @Input() uppercase: boolean = false;
  @Input() enabledCopy: boolean = true;
  @Input() enabledCut: boolean = true;
  @Input() enabledPaste: boolean = true;
  @Input() enabledContextMenu: boolean = true;
  //@Input() mask: TextfieldMask = TextfieldMask.None;
  //@Input() regex: string;

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

  constructor(private cdRef: ChangeDetectorRef) {
    //debugger
  }

  @ViewChild("element") element: ElementRef;

  private consoleLogEvent(event: string) {
    console.log(`${event} => CustomTextAreaComponent => ${Utils.IsValidStringNotWhiteSpace(this.id) ? this.id : ""}`);
  }

  getBoolean(value: any) {
    return Utils.StringToBoolean(value);
  }

  getNumber(value: any) {
    const result = Utils.StringToNumber(value);
    return Utils.IsValidNumber(result) ? result : 0;
  }

  ngOnInit() {
    if (this.enabledLog) {
      this.consoleLogEvent("ngOnInit");
      //console.log("this.mask: " + this.mask);
    }
    
    /*if (this.mask === TextfieldMask.Email) {
      this.type = "email";
    }
    else if (this.mask === TextfieldMask.OnlyDigits) {
      this.type = "number";
      this.pattern = "[0-9]*";

      if (Number(this.maxLength) > 0) {
        const maxLength = Number(this.maxLength);

        this.min = "0";

        let sMaxLength = "";

        for (let index = 0; index < maxLength; index++) {
            sMaxLength = sMaxLength + "9";
        }

        this.max = sMaxLength;
      }
    }*/

    this.addValidators();
  }

  private addValidators() {
    const validators = [];

    if (this.getBoolean(this.required)) {
      validators.push(Validators.required);
    }
    
    /*if (Boolean(this.fullLength) && Number(this.maxLength) > 0) {
      validators.push(fullLengthValidator(Number(this.maxLength)));
    }*/

    if (this.getNumber(this.minLength) > 0) {
      validators.push(Validators.minLength(this.getNumber(this.minLength)));
    }

    /*switch (this.mask) {
      case TextfieldMask.AlphaNumeric:
        validators.push(Validators.pattern(/\b[ñáéíóúa-zÑÁÉÍÓÚA-Z0-9-&-'"'\s,]*\b/));
        break;
      case TextfieldMask.Currency:
        validators.push(Validators.pattern(/\b[0-9]{1,3}([\,][0-9]{3})*[\.][0-9]{2}\b/));
        break;
      case TextfieldMask.Date:
        validators.push(Validators.pattern(/\b\d{2}\/\d{2}\/\d{4}\b/));
        break;
      case TextfieldMask.Decimal:
        validators.push(Validators.pattern(/\d+(\.\d{1,3})?$/));
        break;
      case TextfieldMask.Email:
        validators.push(Validators.email);
        break;
      case TextfieldMask.OnlyDigits:
        validators.push(Validators.pattern(/\b[0-9]+\b/));
        break;
      case TextfieldMask.OnlyLetters:
        validators.push(Validators.pattern(/\b[0-9]+\b/));
        break;
      case TextfieldMask.Regex:
        validators.push(Validators.pattern(new RegExp(this.regex, "i")));
        break;
    }*/

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
  
  onKeyUp(event: any) {
    if (this.enabledLog) {
      this.consoleLogEvent("onKeyUp");
    }

    this.keyup.emit({component: this, event: event});
  }
  
  onKeyDown(event: any) {
    if (this.enabledLog) {
      this.consoleLogEvent("onKeyDown");
    }

    this.keydown.emit({component: this, event: event});
  }

  /*onKeyPress(event: any) {
    if (this.enabledLog) {
      console.log("onKeyPress");
    }

    this.keyPressEmitter.emit({component: this, event: event});
  }*/

  onBlur(event: any) {
    if (this.enabledLog) {
      this.consoleLogEvent("onBlur");
    }

    this.blur.emit({component: this, event: event});
  }

  onInput(event: any) {
    if (this.enabledLog) {
      this.consoleLogEvent("onInput");
      console.log("this.uppercase: " + this.uppercase);
      //console.log("this.mask: " + this.mask);
    }

    this.onChangeInput(event);
  }

  onChange(event: any) {
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
    if (this.enabledLog) {
      this.consoleLogEvent("onCopy");
      console.log("this.enabledCopy: " + this.enabledCopy);
    }

    if (!this.getBoolean(this.enabledCopy)) {
      event.preventDefault();
    }
  }

  onCut(event: any) {
    if (this.enabledLog) {
      this.consoleLogEvent("onCut");
      console.log("this.enabledCut: " + this.enabledCut);
    }

    if (!this.getBoolean(this.enabledCut)) {
      event.preventDefault();
    }
  }

  onPaste(event: any) {
    if (this.enabledLog) {
      this.consoleLogEvent("onPaste");
      console.log("this.enabledPaste: " + this.enabledPaste);
    }

    if (!this.getBoolean(this.enabledPaste)) {
      event.preventDefault();
    }
  }

  onContextMenu(event: any) {
    if (this.enabledLog) {
      this.consoleLogEvent("onContextMenu");
      console.log("this.enabledContextMenu: " + this.enabledContextMenu);
    }

    if (!this.getBoolean(this.enabledContextMenu)) {
      event.preventDefault();
    }
  }

  getErrorMessage() {
    let result = "";

    if (this.control.hasError("required")) {
      result = "Ningún valor ingresado en un campo obligatorio";
    }

    if (this.control.hasError("minlength")) {
      result = `La longitud mínima requerida para este campo es de ${this.minLength} caracteres`;
    }

    /*if (this.control.hasError("fulllength")) {
      result = `La longitud válida para este campo es de ${this.maxLength} caracteres`;
    }
    
    if (this.control.hasError("pattern")) {
      switch (this.mask) {
        case TextfieldMask.AlphaNumeric:
          result = "Este campo alfanumérico no tiene un formato válido";
          break;
        case TextfieldMask.Currency:
          result = "Este campo de moneda no tiene un formato válido";
          break;
        case TextfieldMask.Date:
          result = "Este campo de fecha no tiene un formato válido: dd/mm/yyyy";
          break;
        case TextfieldMask.Decimal:
          result = "Este valor decimal no es válido";
          break;
        case TextfieldMask.OnlyDigits:
          result = "Solo se permiten números en este campo";
          break;
        case TextfieldMask.OnlyLetters:
          result = "Este campo no tiene un formato válido";
          break;
        case TextfieldMask.Regex:
          result = "Este campo no tiene un formato válido";
          break;
      }
    }

    if (this.control.hasError("email")) {
      result = "Este correo electrónico no tiene un formato válido";
    }*/

    return result;
  }

  getType(): string {
    return "CustomTextAreaComponent";
  }

  private modelChangeEmit() {
    this.modelChange.emit(this.model);
    this.change.emit({ component: this, event: event });
  }

  set value(value: string) {
    this.model = value;
    this.cdRef.detectChanges();
    this.modelChangeEmit();
  }

  get value(): string {
    return this.model;
  }
}
