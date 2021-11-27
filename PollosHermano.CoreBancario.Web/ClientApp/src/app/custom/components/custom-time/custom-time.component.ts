import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output, ViewChild } from "@angular/core";
import { ControlValueAccessor, FormControl } from "@angular/forms";
import { NgbTimepicker, NgbTimeStruct } from "@ng-bootstrap/ng-bootstrap";
import Utils from "../../../shared/helpers/Utils";
import { ICustomComponent } from "../../interfaces/ICustomComponent";
import { IEnabled } from "../../interfaces/IEnabled";
import { IRequired } from "../../interfaces/IRequired";
import { IVisible } from "../../interfaces/IVisible";
import { isValidTimeValidator } from "../../validators/isValidTimeValidator";
import { requiredTimeValidator } from "../../validators/requiredTimeValidator";

@Component({
  selector: "custom-time",
  templateUrl: "./custom-time.component.html",
  styleUrls: ["./custom-time.component.scss"]
})
export class CustomTimeComponent
  implements
  ICustomComponent,
  OnInit,
  IVisible,
  IEnabled,
  IRequired,
  ControlValueAccessor {
  //privadas
  //underlyingElement: any;

  //public
  @Input() id: string;
  @Input() tag: string;
  private _model: NgbTimeStruct = null;
  public get model(): NgbTimeStruct {
    return this._model;
  }
  @Input()
  public set model(model: NgbTimeStruct) {
    this._model = model;
    this.control.updateValueAndValidity();
  }

  @Output() modelChange = new EventEmitter<any>();
  @Input() visible: boolean = true;
  @Input() readonly: boolean = true;

  private _enabled: boolean = true;

  public get enabled(): boolean {
    return this._enabled;
  }

  @Input()
  public set enabled(enabled: boolean) {
    this._enabled = this.getBoolean(enabled);
    this.setEnabledInputs();
  }
  @Input() required: boolean = false;

  @Input() name: string = "";

  @Input() seconds: boolean = true;

  @Input() metadata: any;

  control: FormControl = new FormControl("", []);



  enabledLog: boolean = false;

  constructor(private cdRef: ChangeDetectorRef) {

  }

  writeValue(obj: any): void {
    if (this.underlyingElement) {
      this.underlyingElement.writeValue(obj);
    }
  }

  registerOnChange(fn: any): void {
    if (this.underlyingElement) {
      this.underlyingElement.registerOnChange(fn);
    }
  }
  registerOnTouched(fn: any): void {
    if (this.underlyingElement) {
      this.underlyingElement.registerOnTouched(fn);
    }
  }

  setDisabledState?(isDisabled: boolean): void {
    if (this.underlyingElement) {
      this.underlyingElement.setDisabledState(isDisabled);
    }
  }

  @ViewChild("underlyingElement") underlyingElement: NgbTimepicker;

  private consoleLogEvent(event: string) {
    console.log(`${event} => CustomTimeComponent => ${Utils.IsValidStringNotWhiteSpace(this.id) ? this.id : ""}`);
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
    }

    this.addValidators();
  }

  ngAfterViewInit() {
    this.setEnabledInputs();
  }

  private setEnabledInputs() {
    if (this.underlyingElement) {
      this.underlyingElement.disabled = !this.enabled;
      this.cdRef.detectChanges();
    }
  }

  private addValidators() {
    //debugger
    const validators = [];

    if (this.getBoolean(this.required)) {
      validators.push(requiredTimeValidator(this));
    }

    validators.push(isValidTimeValidator(this));

    if (validators.length > 0) {
      this.control = new FormControl("", validators);
    }
  }

  getErrorMessage() {
    if (this.control.hasError("requiredTimeValidator")) {
      return "Ningún valor ingresado en un campo obligatorio";
    }

    if (this.control.hasError("isValidTimeValidator")) {
      return  "El formato es inválido";
    }

    return "";
  }

  getType(): string {
    return "CustomTimeComponent";
  }

  set value(value: string) {
    try {
      if (Utils.IsValidStringNotWhiteSpace(value)) {
        const items = value.split(":");
        if (items.length === 3) {
          const sHour = items[0];
          const sMinute = items[1];
          const sSecond = items[2];

          const hour = Number(sHour);
          const minute = Number(sMinute);
          const second = Number(sSecond);

          if ((hour >= 0 && hour <= 23) &&
              (minute >= 0 && minute <= 59) &&
            (second >= 0 && second <= 59)) {

            this.model = {
              hour: hour,
              minute: minute,
              second: second
            };

          }
        }
      }
    }
    catch (error) {
      console.log(error);
    }
  }

  get value(): string {
    return this.model !== undefined && this.model !== null ?
      (this.model.hour.toString().length == 1 ? ("0" + this.model.hour.toString()) : this.model.hour.toString()) + ":" + (this.model.minute.toString().length == 1 ? ("0" + this.model.minute.toString()) : this.model.minute.toString()) + ":" + (this.model.second.toString().length == 1 ? "0" + this.model.second.toString() : this.model.second.toString()) : null;
  }
}
