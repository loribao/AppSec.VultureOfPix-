fn main() {
    csbindgen::Builder::default()
        .input_extern_file("./src/lib.rs")
        .csharp_dll_name("appsec_infra")         // required
        .csharp_class_name("NativeMethods")     // optional, default: NativeMethods
        .csharp_namespace("NativeApiBind")          // optional, default: CsBindgen
        .csharp_class_accessibility("internal") // optional, default: internal
        .generate_csharp_file("../AppSec.Infra/Data/Drivers/NativeMethods.g.cs")
        .unwrap();
}