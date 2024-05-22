//https://github.com/loribao/app_demo_dotnet_elasticagent_sonar.git
use git2::{Cred, Error, RemoteCallbacks};
use std::env;
use std::path::Path;

fn main() {
    // Prepare callbacks.
    let mut callbacks = RemoteCallbacks::new();

    callbacks.credentials(|_url, username_from_url, _allowed_types| {
        let pubkey_str = format!("{}/source/repos/AppSec.VultureOfPix-/loribao.pub", env::var("HOME").unwrap());
        let seckey_str = format!("{}/source/repos/AppSec.VultureOfPix-/loribao", env::var("HOME").unwrap());
        print!("pubkey_str: {}\n", pubkey_str);
        print!("seckey_str: {}\n", seckey_str);
        print!("username_from_url: {:?}\n", username_from_url );
        print!("username_from_url: {:?}\n", _allowed_types);
        let pubkey = Path::new(&pubkey_str);
        let seckey = Path::new(&seckey_str);

        Cred::ssh_key(
            username_from_url.unwrap(),
            Some(pubkey),
            seckey,
            None,
        )
    });

    // Prepare fetch options.
    let mut fo = git2::FetchOptions::new();

    fo.remote_callbacks(callbacks);

    // Prepare builder.
    let mut builder = git2::build::RepoBuilder::new();
    builder.fetch_options(fo);

    // Clone the project.
    let rep = builder.clone(
        "git@github.com:loribao/app_demo_dotnet_elasticagent_sonar.git",
        Path::new(&format!("{}\\Downloads\\tes", env::var("HOME").unwrap())),
    );
    let _ = rep.unwrap().find_branch("dev", git2::BranchType::Local).unwrap();

}
