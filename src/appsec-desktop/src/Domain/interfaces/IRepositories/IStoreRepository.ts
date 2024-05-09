import KeyValue from "../../models/KeyValue";

interface IStoreRepository {
    delete(key:string): Promise<void>;
    insert(store: KeyValue): Promise<void>;
    get(): Promise<KeyValue[]>;
}

export default IStoreRepository;
