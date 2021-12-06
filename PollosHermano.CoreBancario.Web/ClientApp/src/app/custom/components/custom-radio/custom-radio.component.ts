import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { FormControl, Validators } from "@angular/forms";
import Utils from "../../../shared/helpers/Utils";
import { ICustomComponent } from "../../interfaces/ICustomComponent";
import { IEnabled } from "../../interfaces/IEnabled";
import { IRequired } from "../../interfaces/IRequired";
import { IVisible } from "../../interfaces/IVisible";

@Component({
  selector: "custom-radio",
  templateUrl: "./custom-radio.component.html",
  styleUrls: ["./custom-radio.component.scss"]
})
export class CustomRadioComponent
  implements
  ICustomComponent,
  OnInit,
  IVisible,
  IEnabled,
  IRequired {

  @Input() id: string;
  @Input() tag: string;
  @Input() model: any;
  @Output() modelChange = new EventEmitter<any>();
  //@Input() enabled: string = "true";
  @Input() visible: boolean = true;
  @Input() enabled: boolean = true;
  @Input() required: boolean = false;

  @Input() dataSource: any;
  @Output() dataSourceChange = new EventEmitter<any>();

  @Input() bindLabel: string;
  @Input() bindValue: string = "";
  @Input() name: string = "";

  @Input() group: string;


  //@Input() group: string;


  @Output() change = new EventEmitter<any>();

  //@Input() visible: string = "true";

  @Input() metadata: any;


  control: FormControl = new FormControl("", []);
  
  enabledLog: boolean = false;

  constructor(private cdRef: ChangeDetectorRef) {
    //debugger
  }

  private consoleLogEvent(event: string) {
    console.log(`${event} => CustomRadioComponent => ${Utils.IsValidStringNotWhiteSpace(this.id) ? this.id : ""}`);
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
    }

    this.addValidators();
  }
  
  private addValidators() {
    //debugger
    const validators = [];

    if (this.getBoolean(this.required)) {
      validators.push(Validators.required);
    }
    
    if (validators.length > 0) {
      this.control = new FormControl("", validators);
    }
  }

  onChange(event: any) {
    //debugger
    if (this.enabledLog) {
      this.consoleLogEvent("onChange");
    }

    this.modelChange.emit(this.model);
    this.change.emit({ component: this, event: event });
  }

  private modelChangeEmit() {
    this.modelChange.emit(this.model);
    this.change.emit({ component: this, event: event });
  }

  getErrorMessage() {
    //debugger
    let result = "";

    if (this.control.hasError("required")) {
      result = "Ningún valor ingresado en un campo obligatorio";
    }
    
    return result;
  }

  getBindLabel(item: any) {
    return Utils.IsValidStringNotWhiteSpace(this.bindLabel) ? item[this.bindLabel] : item;
  }

  getLabel() {
    return this.model === undefined || this.model === null ? this.model : (Utils.IsValidStringNotWhiteSpace(this.bindLabel) ? this.model[this.bindLabel] : this.model);
  }

  getBindValue(item: any) {
    return Utils.IsValidStringNotWhiteSpace(this.bindValue) ? item[this.bindValue] : item;
  }

  setValue(model: any) {
    if (Utils.IsValidStringNotWhiteSpace(this.bindValue)) {
      this.model[this.bindValue] = model;
    }
    else {
      this.model = model;
    }
  }

  getValue(): any {
    return this.model;
  }

  getType(): string {
    return "CustomRadioComponent";
  }

  set value(model: any) {
    if (!Utils.IsValidArray(this.dataSource)
      || this.dataSource.length === 0) {
      return;
    }

    for (let i = 0; i < this.dataSource.length; i++) {
      let item = this.dataSource[i];
      let value = item;
      if (value === model) {
        this.model = item;
        this.cdRef.detectChanges();
        this.modelChangeEmit();
        break;
      }
    }
  }

  get value(): any {
    return this.getValue();
  }
}
