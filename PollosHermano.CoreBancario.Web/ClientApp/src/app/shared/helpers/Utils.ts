export default class Utils {
  static ObjectId() {
    var timestamp = (new Date().getTime() / 1000 | 0).toString(16);
    return timestamp + 'xxxxxxxxxxxxxxxx'.replace(/[x]/g, function () {
      return (Math.random() * 16 | 0).toString(16);
    }).toLowerCase();
  };

  static IsValidFunction(value: any) {
    return !this.IsUndefined(value) && !this.IsNull(value) && this.IsFunction(value);
  }

  static IsValidString(value: any) {
    return !this.IsUndefined(value) && !this.IsNull(value) && this.IsString(value);
  }

  static IsValidStringNotEmpty(value: any) {
    return this.IsValidString(value) && value;
  }

  static IsValidStringNotWhiteSpace(value: any) {
    return this.IsValidString(value) && value.trim();
  }

  static IsValidNumber(value: any) {
    return !this.IsUndefined(value) && !this.IsNull(value) && this.IsNumber(value);
  }

  static IsValidBoolean(value: any) {
    return !this.IsUndefined(value) && !this.IsNull(value) && this.IsBoolean(value);
  }

  static IsValidArray(value: any) {
    return !this.IsUndefined(value) && !this.IsNull(value) && this.IsArray(value);
  }

  static IsValidRegExp(value: any) {
    return !this.IsUndefined(value) && !this.IsNull(value) && this.IsRegExp(value);
  }

  static IsValidObject(value: any) {
    return !this.IsUndefined(value) && !this.IsNull(value) && this.IsObject(value);
  }

  static IsValidJsonObject(value: any) {
    return !this.IsUndefined(value) && !this.IsNull(value) && this.IsJsonObject(value);
  }

  static IsValidUrl(value: any) {
    return this.IsValidString(value) && this.IsUrl(value);
  }

  static IsString(value: any) {
    return typeof value === "string" || value instanceof String;
  }

  static IsNumber(value: any) {
    return typeof value === "number" && isFinite(value);
  }

  static IsArray(value: any) {
    return value && typeof value === "object" && value.constructor === Array;
  }

  static IsFunction(value: any) {
    return typeof value === "function";
  }

  static IsObject(value: any) {
    return value && typeof value === "object" && value.constructor === Object;
  }

  static IsJsonObject(value: any) {
    return value && value.constructor === {}.constructor;
  }

  static IsNull(value: any) {
    return value === null;
  }

  static IsUndefined(value: any) {
    return typeof value === "undefined";
  }

  static IsBoolean(value: any) {
    return typeof value === "boolean";
  }

  static IsRegExp(value: any) {
    return value && typeof value === "object" && value.constructor === RegExp;
  }

  static IsError(value: any) {
    return value instanceof Error && typeof value.message !== "undefined";
  }

  static IsDate(value: any) {
    return value instanceof Date;
  }

  static IsSymbol(value: any) {
    return typeof value === "symbol";
  }

  static IsUrl(value: any) {
    const pattern = new RegExp("^(https?:\\/\\/)?" + // protocol
      "((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.)+[a-z]{2,}|" + // domain name
      "((\\d{1,3}\\.){3}\\d{1,3}))" + // ip (v4) address
      "(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*" + // port and path
      "(\\?[;&a-z\\d%_.~+=-]*)?" + // query string
      "(\\#[-a-z\\d_]*)?$", "i"); // fragment locator
    return !!pattern.test(value);
  }

  static StringToBoolean(value: any) {
    return this.IsValidBoolean(value) ? value : (this.IsValidStringNotWhiteSpace(value) && value.trim().toLowerCase() === "true");
  }

  static StringToNumber(value: any) {
    if (this.IsValidNumber(value)) {
      return value;
    }

    let result = undefined;

    try {
      if (this.IsValidStringNotWhiteSpace(value)) {
        result = Number(value);
      }
    }
    catch (err) {

    }

    return result;
  }

  static FirstCharToLowerCase(value: string) {
    return this.IsValidStringNotEmpty(value) ? value.charAt(0).toLowerCase() + value.slice(1) : value;
  }

  static HasDecimalPlace(value: string, x: number) {
    var pointIndex = value.indexOf('.');
    return pointIndex >= 0 && pointIndex < value.length - x;
  }

  static ObjectEquals(x: any, y: any) {
    const that = this;

    if (x === null || x === undefined || y === null || y === undefined) { return x === y; }
    // after this just checking type of one would be enough
    if (x.constructor !== y.constructor) { return false; }
    // if they are functions, they should exactly refer to same one (because of closures)
    if (x instanceof Function) { return x === y; }
    // if they are regexps, they should exactly refer to same one (it is hard to better equality check on current ES)
    if (x instanceof RegExp) { return x === y; }
    if (x === y || x.valueOf() === y.valueOf()) { return true; }
    if (Array.isArray(x) && x.length !== y.length) { return false; }

    // if they are dates, they must had equal valueOf
    if (x instanceof Date) { return false; }

    // if they are strictly equal, they both need to be object at least
    if (!(x instanceof Object)) { return false; }
    if (!(y instanceof Object)) { return false; }

    // recursive object equality check
    var p = Object.keys(x);
    return Object.keys(y).every(function (i) { return p.indexOf(i) !== -1; }) &&
      p.every(function (i) { return that.ObjectEquals(x[i], y[i]); });
  }
}
