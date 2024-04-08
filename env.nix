with import <nixpkgs>{};

mkShell {
  name = "appsec-env";
  buildInputs = with pkgs; [
    (with dotnetCorePackages; sdk_8_0)
    rustc
    cargo
    python3
    julia
    openjdk17
    firefox
    zap
  ];
}
