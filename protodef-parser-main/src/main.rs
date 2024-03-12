
use std::fs::{self, read};
use std::fs::File;

use linked_hash_map::LinkedHashMap;
use protodef_parser::{DataType, Namespace, Structure};
use serde_json::Map;

macro_rules! cast {
    ($target: expr, $pat: path) => {
        {
            if let $pat(a) = $target { // #1
                a
            } else {
                panic!(
                    "mismatch variant when cast to {}", 
                    stringify!($pat)); // #2
            }
        }
    };
}

fn main() 						
{		
    				
    let file = File::open("test/protocol.json").expect("asd");

    let protocol = protodef_parser::read_protocol(&file).expect("asd");

    //generate_types(protocol.types);

    let play = cast!(&protocol.namespaces["play"], Namespace::Map);

    let to_server = cast!(&play["toServer"], Namespace::Map);

    let to_server_packets = cast!(&to_server["types"], Namespace::Map);

    for (k, v) in to_server_packets
    {
        let packet = cast!(v, Namespace::DataType);

        if k.starts_with("packet_")
        {
            let fields = cast!(cast!(packet, DataType::Structure).as_ref(), Structure::Container);
            
            let mut ok = true;

            for field in fields
            {
                if let DataType::Numeric(_num) = &field.field_type
                {
                    
                }
                else {
                    ok = false;
                    break;
                }
            }

            if ok
            {

            }
            
        }

    }

}

fn check_packet_for_types(packet: Box<Structure>) -> bool
{
    

    return true;
}

fn generate_types(types: LinkedHashMap<String, DataType>) {
    for (k, v) in types
    {  
        

        if let DataType::Custom(_name) = v {
            println!("{}", _name);
        } else if let DataType::Structure(_struct) = v {
            match *_struct {
                Structure::Array{..}=>println!("Array: {}", k),
                Structure::Container{..}=> println!("Container: {}", k),
                Structure::Count{..}=> println!("Count: {}",k),
            }
        }
    }
}