import { injectable } from "tsyringe";

@injectable()
class Store {
    public token: string="";
    public baseUrlBackend: string = "http://localhost:5048/";
    public baseUrlApiSonar: string = "http://localhost:9000/"
}

export default Store;
