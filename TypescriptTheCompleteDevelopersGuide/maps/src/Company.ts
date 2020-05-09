import faker from "faker";
import { IMappable } from "./Map";

export class Company implements IMappable {
  companyName: string;
  catchPhrase: string;
  location: {
    lat: number;
    lng: number;
  };
  color: string = "blue";

  constructor() {
    this.companyName = faker.company.companyName();
    this.catchPhrase = faker.company.catchPhrase();
    this.location = {
      lat: +faker.address.latitude(),
      lng: +faker.address.longitude(),
    };
  }

  markerContent(): string {
    return `The Company name is ${this.companyName} \n <i>${this.catchPhrase}</i>`;
  }
}
