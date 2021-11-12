import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from "@angular/core";
import { FormControl } from "@angular/forms";
import Utils from "../../../shared/helpers/Utils";
import { ICustomComponent } from "../../interfaces/ICustomComponent";

@Component({
  selector: "custom-hidden",
  templateUrl: "./custom-hidden.component.html",
  styleUrls: ["./custom-hidden.component.scss"]
})
export class CustomHiddenComponent
  implements
  ICustomComponent,
  OnInit {

  @Input() id: string;
  @Input() model: string = "";
  @Output() modelChange = new EventEmitter<string>();
  @Input() name: string = "";
  control: FormControl;

  @Input() metadata: any;
  
  enabledLog: boolean = false;

  constructor() {
    //debugger
  }

  @ViewChild("element") element: ElementRef;

  private consoleLogEvent(event: string) {
    console.log(`${event} => CustomHiddenComponent => ${Utils.IsValidStringNotWhiteSpace(this.id) ? this.id : ""}`);
  }

  ngOnInit() {
    if (this.enabledLog) {
      this.consoleLogEvent("ngOnInit");
    }
  }

  getType(): string {
    return "CustomHiddenComponent";
  }

  set value(value: string) {
    this.model = value;
    this.modelChange.emit(this.model);
  }

  get value(): string {
    return this.model;
  }
}
