String.prototype.withComma = function() {
  return this ? this.replace(/\B(?=(\d{3})+(?!\d))/g, ",") : this;
};
Number.prototype.withComma = function() {
  return this ? this.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") : this;
};
String.prototype.toCurrency = function(currencyUnit) {
  return this ? this.withComma() + " " + currencyUnit : this;
};
Number.prototype.toCurrency = function(currencyUnit) {
  return this ? this.withComma() + " " + currencyUnit : this;
};

declare global {
  interface String {
    withComma: () => string;
    toCurrency: (currencyUnit: string) => string;
  }
  interface Number {
    withComma: () => string;
    toCurrency: (currencyUnit: string) => string;
  }
}

export default "";
