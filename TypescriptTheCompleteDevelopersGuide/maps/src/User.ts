import faker from "faker";
import { IMappable } from "./Map";

export class User implements IMappable {
  name: string;
  location: {
    lat: number;
    lng: number;
  };
  color: string = "red";

  constructor() {
    this.name = faker.name.firstName();
    this.location = {
      lat: +faker.address.latitude(),
      lng: +faker.address.longitude(),
    };
  }

  markerContent(): string {
    return `User name is ${this.name}`;
  }
}
