import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { FormControl, Validators } from "@angular/forms";
import Utils from "../../../shared/helpers/Utils";
import { ICustomComponent } from "../../interfaces/ICustomComponent";
import { IEnabled } from "../../interfaces/IEnabled";
import { IRequired } from "../../interfaces/IRequired";
import { IVisible } from "../../interfaces/IVisible";

@Component({
  selector: "custom-combo",
  templateUrl: "./custom-combo.component.html",
  styleUrls: ["./custom-combo.component.scss"]
})
export class CustomComboComponent
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
  @Input() visible: boolean = true;
  @Input() enabled: boolean = true;
  @Input() required: boolean = false;

  @Input() dataSource: any;
  @Output() dataSourceChange = new EventEmitter<any>();

  @Input() bindLabel: string = "";
  @Input() bindValue: string = "";

  @Input() name: string = "";
  


  @Output() change = new EventEmitter<any>();

  //@Input() visible: string = "true";

  @Input() metadata: any;
  

  control: FormControl = new FormControl("", []);

  enabledLog: boolean = false;

  constructor(private cdRef: ChangeDetectorRef) {
    //debugger
  }

  private consoleLogEvent(event: string) {
    console.log(`${event} => CustomComboComponent => ${Utils.IsValidStringNotWhiteSpace(this.id) ? this.id : ""}`);
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
    if (this.enabledLog) {
      this.consoleLogEvent("ngOnInit");
    }
    
    this.addValidators();
  }

  private addValidators() {
    const validators = [];

    if (this.getBoolean(this.required)) {
      validators.push(Validators.required);
    }
    
    if (validators.length > 0) {
      this.control = new FormControl("", validators);
    }
  }

  onChange(event: any) {
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
    return this.model === undefined || this.model === null ? this.model : (Utils.IsValidStringNotWhiteSpace(this.bindValue) ? this.model[this.bindValue] : this.model);
  }

  getType(): string {
    return "CustomComboComponent";
  }

  set value(model: any) {
    if (!Utils.IsValidArray(this.dataSource)
        || this.dataSource.length === 0) {
      return;
    }

    let byProperty = Utils.IsValidStringNotWhiteSpace(this.bindValue);

    for (let i = 0; i < this.dataSource.length; i++) {
      let item = this.dataSource[i];
      let value = byProperty ? item[this.bindValue] : item;
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
