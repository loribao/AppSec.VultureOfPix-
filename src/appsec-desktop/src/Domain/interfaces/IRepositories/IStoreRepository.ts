import Store from "../../../Data/stores/Store";

interface IStoreRepository {
    pop(): Promise<Store>;
    push(store: Store): Promise<void>;
    get(): Promise<Store>;
}

export default IStoreRepository;
