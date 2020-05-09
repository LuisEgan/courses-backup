import { Map } from "./Map";
import { User } from "./User";
import { Company } from "./Company";

const map = new Map("map");
const user = new User();
const company = new Company();

map.addMarker(user);
map.addMarker(company);
