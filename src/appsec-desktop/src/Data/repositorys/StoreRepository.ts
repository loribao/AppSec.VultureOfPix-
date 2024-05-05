import { inject, injectable } from "tsyringe";
import type IStoreRepository from "../../Domain/interfaces/IRepositories/IStoreRepository";
import Store from "../stores/Store";
import type IDriver from "../../Domain/interfaces/IDrivers/IDriver";

@injectable()
class StoreRepository implements IStoreRepository{
    constructor(@inject("IDriver") private driver: IDriver) {}
    pop(): Promise<Store> {
        throw new Error("Method not implemented.");
    }
    push(store: Store): Promise<void> {
        throw new Error("Method not implemented.");
    }
    get(): Promise<Store> {
        throw new Error("Method not implemented.");
    }
}

export default StoreRepository;
