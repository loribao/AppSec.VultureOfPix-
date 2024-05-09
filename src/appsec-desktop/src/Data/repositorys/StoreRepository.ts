import { inject, injectable } from "tsyringe";
import type IStoreRepository from "../../Domain/interfaces/IRepositories/IStoreRepository";
import type IDriver from "../../Domain/interfaces/IDrivers/IDriver";
import Store from "../../Domain/models/KeyValue";

@injectable()
class StoreRepository implements IStoreRepository{
    constructor(@inject("IDriver") private driver: IDriver) {}
    async delete(key: string): Promise<void> {
      await this.driver.Store.delete(key);
    }
    async insert({id,key,value}: Store): Promise<void> {
        await this.driver.Store.save(id,key,value);
    }
    async get(): Promise<Store[]> {
        return await this.driver.Store.loadStorageAsync();
    }
}

export default StoreRepository;
