use linked_hash_map::LinkedHashMap;
use serde::de;
use serde::de::{SeqAccess, Unexpected, Visitor};
use serde::{Deserialize, Deserializer};
use std::collections::HashMap;
use std::fmt;
use std::io::Read;



pub fn read_protocol<R: Read>(reader: R) -> serde_json::Result<Protocol> 
{
    serde_json::from_reader(reader)
}

#[derive(Debug, Eq, PartialEq, Deserialize)]
pub struct Protocol {
    pub types: LinkedHashMap<String, DataType>,
    #[serde(flatten)]
    pub namespaces: LinkedHashMap<String, Namespace>,
}

#[derive(Debug, Eq, PartialEq, Deserialize)]
#[serde(untagged)]
pub enum Namespace {
    Map(LinkedHashMap<String, Namespace>),
    DataType(DataType),
}

#[derive(Debug, Eq, PartialEq, Deserialize)]
#[serde(untagged)]
pub enum DataType {
    Conditional(Box<Conditional>),
    Numeric(Numeric),
    Primitive(Primitive),
    Structure(Box<Structure>),
    Util(Box<Util>),
    Custom(String),
}

#[derive(Debug, Eq, PartialEq)]
pub enum Conditional {
    Switch(Switch),
    Option(DataType),
}

#[derive(Debug, Eq, PartialEq, Deserialize)]
pub struct Switch {
    name: Option<String>,
    #[serde(rename = "compareTo")]
    compare_to: String,
    fields: LinkedHashMap<String, DataType>,
    default: Option<DataType>,
}

#[derive(Debug, Eq, PartialEq)]
pub enum Numeric {
    Byte { signed: bool },
    Short { signed: bool, byte_order: ByteOrder },
    Int { signed: bool, byte_order: ByteOrder },
    Long { signed: bool, byte_order: ByteOrder },
    Float { byte_order: ByteOrder },
    Double { byte_order: ByteOrder },
    VarInt,
}

#[derive(Debug, Eq, PartialEq)]
pub enum ByteOrder {
    BigEndian,
    LittleEndian,
}

#[derive(Debug, Eq, PartialEq, Deserialize)]
#[serde(rename_all = "lowercase")]
pub enum Primitive {
    #[serde(rename = "bool")]
    Boolean,
    #[serde(rename = "cstring")]
    String,
    Void,
}

#[derive(Debug, Eq, PartialEq)]
pub enum Structure {
    /// Represents a list of values with same type.
    Array(Array),
    /// Represents a list of named values.
    Container(Vec<Field>),
    /// Represents a count field for an array or a buffer.
    Count(Count),
}

#[derive(Debug, Eq, PartialEq, Deserialize)]
pub struct Array {
    /// The type of length prefix.
    #[serde(rename = "countType")]
    pub count_type: Option<DataType>,
    /// A reference to the field counting the elements, or a fixed size.
    pub count: Option<ArrayCount>,
    /// The type of the elements.
    #[serde(rename = "type")]
    pub elements_type: DataType,
}

#[derive(Debug, Eq, PartialEq, Deserialize)]
#[serde(untagged)]
pub enum ArrayCount {
    /// Reference to the field counting the elements.
    FieldReference(String),
    /// Array with fixed length.
    FixedLength(u32),
}

#[derive(Debug, Eq, PartialEq, Deserialize)]
pub struct Field {
    pub name: Option<String>,
    #[serde(rename = "type")]
    pub field_type: DataType,
    // Useless.
    #[serde(rename = "anon")]
    anonymous: Option<bool>,
}

#[derive(Debug, Eq, PartialEq, Deserialize)]
pub struct Count {
    /// The type of count.
    #[serde(rename = "type")]
    pub count_type: DataType,
    /// A field to count for.
    #[serde(rename = "countFor")]
    pub count_for: String,
}

#[derive(Debug, Eq, PartialEq)]
pub enum Util {
    Buffer(Buffer),
    Mapper(Mapper),
    Bitfield(Vec<BitField>),
    PrefixedString { count_type: DataType },
    Loop(Box<Loop>),
    TopBitSetTerminatedArray(Box<Structure>),
}

#[derive(Debug, Eq, PartialEq, Deserialize)]
pub struct Buffer {
    /// The type of length prefix.
    #[serde(rename = "countType")]
    pub count_type: Option<DataType>,
    /// A reference to the field counting the elements, or a fixed size.
    pub count: Option<ArrayCount>,
    /// Represent rest bytes as-is.
    pub rest: Option<bool>,
}

#[derive(Debug, Eq, PartialEq, Deserialize)]
pub struct Mapper {
    #[serde(rename = "type")]
    pub mappings_type: String,
    pub mappings: LinkedHashMap<String, String>,
}

#[derive(Debug, Deserialize, PartialEq, Eq)]
pub struct BitField {
    pub name: String,
    pub size: usize,
    pub signed: bool,
}

#[derive(Debug, Eq, PartialEq, Deserialize)]
pub struct Loop {
    #[serde(rename = "endVal")]
    pub end_val: u32,
    #[serde(rename = "type")]
    pub data_type: DataType,
}

struct NumericVisitor;

impl<'de> Visitor<'de> for NumericVisitor {
    type Value = Numeric;

    fn expecting(&self, formatter: &mut fmt::Formatter) -> fmt::Result {
        formatter.write_str("an valid numeric type string")
    }

    fn visit_str<E>(self, value: &str) -> Result<Self::Value, E>
    where
        E: de::Error,
    {
        match value {
            "i8" => Ok(Numeric::Byte { signed: true }),
            "u8" => Ok(Numeric::Byte { signed: false }),
            "i16" => Ok(Numeric::Short {
                signed: true,
                byte_order: ByteOrder::BigEndian,
            }),
            "u16" => Ok(Numeric::Short {
                signed: false,
                byte_order: ByteOrder::BigEndian,
            }),
            "li16" => Ok(Numeric::Short {
                signed: true,
                byte_order: ByteOrder::LittleEndian,
            }),
            "lu16" => Ok(Numeric::Short {
                signed: false,
                byte_order: ByteOrder::LittleEndian,
            }),
            "i32" => Ok(Numeric::Int {
                signed: true,
                byte_order: ByteOrder::BigEndian,
            }),
            "u32" => Ok(Numeric::Int {
                signed: false,
                byte_order: ByteOrder::BigEndian,
            }),
            "li32" => Ok(Numeric::Int {
                signed: true,
                byte_order: ByteOrder::LittleEndian,
            }),
            "lu32" => Ok(Numeric::Int {
                signed: false,
                byte_order: ByteOrder::LittleEndian,
            }),
            "i64" => Ok(Numeric::Long {
                signed: true,
                byte_order: ByteOrder::BigEndian,
            }),
            "u64" => Ok(Numeric::Long {
                signed: false,
                byte_order: ByteOrder::BigEndian,
            }),
            "li64" => Ok(Numeric::Long {
                signed: true,
                byte_order: ByteOrder::LittleEndian,
            }),
            "lu64" => Ok(Numeric::Long {
                signed: false,
                byte_order: ByteOrder::LittleEndian,
            }),
            "f32" => Ok(Numeric::Float {
                byte_order: ByteOrder::BigEndian,
            }),
            "lf32" => Ok(Numeric::Float {
                byte_order: ByteOrder::LittleEndian,
            }),
            "f64" => Ok(Numeric::Double {
                byte_order: ByteOrder::BigEndian,
            }),
            "lf64" => Ok(Numeric::Double {
                byte_order: ByteOrder::LittleEndian,
            }),
            "varint" => Ok(Numeric::VarInt),
            _ => Err(de::Error::invalid_value(Unexpected::Str(&value), &self)),
        }
    }
}

impl<'de> Deserialize<'de> for Numeric {
    fn deserialize<D>(deserializer: D) -> Result<Self, <D>::Error>
    where
        D: Deserializer<'de>,
    {
        deserializer.deserialize_str(NumericVisitor)
    }
}

struct ConditionalVisitor;

impl<'de> Visitor<'de> for ConditionalVisitor {
    type Value = Conditional;

    fn expecting(&self, formatter: &mut fmt::Formatter) -> fmt::Result {
        formatter.write_str("an valid `Conditional`")
    }

    fn visit_seq<A>(self, mut seq: A) -> Result<Self::Value, <A as SeqAccess<'de>>::Error>
    where
        A: SeqAccess<'de>,
    {
        let conditional_type: String = seq
            .next_element()?
            .ok_or_else(|| de::Error::invalid_length(0, &self))?;

        match conditional_type.as_str() {
            "switch" => {
                let switch = seq
                    .next_element()?
                    .ok_or_else(|| de::Error::invalid_length(1, &self))?;

                Ok(Conditional::Switch(switch))
            }
            "option" => {
                let data_type = seq
                    .next_element()?
                    .ok_or_else(|| de::Error::invalid_length(1, &self))?;

                Ok(Conditional::Option(data_type))
            }
            unknown_variant => {
                // Attempt to find switch with wrong type.
                let mut map: HashMap<String, String> = seq.next_element()?.ok_or_else(|| {
                    de::Error::unknown_variant(unknown_variant, &["switch", "option"])
                })?;

                if map.len() == 1 {
                    if let Some(compare_to) = map.remove("compareTo") {
                        let switch = Switch {
                            name: Some(unknown_variant.to_owned()),
                            compare_to,
                            fields: LinkedHashMap::new(),
                            default: None,
                        };

                        return Ok(Conditional::Switch(switch));
                    }
                }

                Err(de::Error::unknown_variant(
                    unknown_variant,
                    &["switch", "option"],
                ))
            }
        }
    }
}

impl<'de> Deserialize<'de> for Conditional {
    fn deserialize<D>(deserializer: D) -> Result<Self, <D>::Error>
    where
        D: Deserializer<'de>,
    {
        deserializer.deserialize_seq(ConditionalVisitor)
    }
}

struct StructureVisitor;

impl<'de> Visitor<'de> for StructureVisitor {
    type Value = Structure;

    fn expecting(&self, formatter: &mut fmt::Formatter) -> fmt::Result {
        formatter.write_str("an valid `Structure`")
    }

    fn visit_seq<A>(self, mut seq: A) -> Result<Self::Value, <A as SeqAccess<'de>>::Error>
    where
        A: SeqAccess<'de>,
    {
        let struct_type: String = seq
            .next_element()?
            .ok_or_else(|| de::Error::invalid_length(0, &self))?;

        match struct_type.as_str() {
            "container" => {
                let fields = seq
                    .next_element()?
                    .ok_or_else(|| de::Error::invalid_length(1, &self))?;

                Ok(Structure::Container(fields))
            }
            "array" => {
                let array = seq
                    .next_element()?
                    .ok_or_else(|| de::Error::invalid_length(1, &self))?;

                Ok(Structure::Array(array))
            }
            "count" => {
                let count = seq
                    .next_element()?
                    .ok_or_else(|| de::Error::invalid_length(1, &self))?;

                Ok(Structure::Count(count))
            }
            unknown_variant => Err(de::Error::unknown_variant(
                unknown_variant,
                &["container", "array", "count"],
            )),
        }
    }
}

impl<'de> Deserialize<'de> for Structure {
    fn deserialize<D>(deserializer: D) -> Result<Self, <D>::Error>
    where
        D: Deserializer<'de>,
    {
        deserializer.deserialize_seq(StructureVisitor)
    }
}

struct UtilVisitor;

impl<'de> Visitor<'de> for UtilVisitor {
    type Value = Util;

    fn expecting(&self, formatter: &mut fmt::Formatter) -> fmt::Result {
        formatter.write_str("an valid util")
    }

    fn visit_seq<A>(self, mut seq: A) -> Result<Self::Value, <A as SeqAccess<'de>>::Error>
    where
        A: SeqAccess<'de>,
    {
        let util_type: String = seq
            .next_element()?
            .ok_or_else(|| de::Error::invalid_length(0, &self))?;

        match util_type.as_str() {
            "buffer" => {
                let buffer = seq
                    .next_element()?
                    .ok_or_else(|| de::Error::invalid_length(1, &self))?;

                Ok(Util::Buffer(buffer))
            }
            "mapper" => {
                let mapper = seq
                    .next_element()?
                    .ok_or_else(|| de::Error::invalid_length(1, &self))?;

                Ok(Util::Mapper(mapper))
            }
            "bitfield" => {
                let bitfields = seq
                    .next_element()?
                    .ok_or_else(|| de::Error::invalid_length(1, &self))?;

                Ok(Util::Bitfield(bitfields))
            }
            "pstring" => {
                let mut map: HashMap<String, DataType> = seq
                    .next_element()?
                    .ok_or_else(|| de::Error::invalid_length(1, &self))?;

                let count_type = map
                    .remove("countType")
                    .ok_or_else(|| de::Error::missing_field("countType"))?;

                Ok(Util::PrefixedString { count_type })
            }
            "topBitSetTerminatedArray" => {
                let mut value: HashMap<String, Structure> = seq
                    .next_element()?
                    .ok_or_else(|| de::Error::invalid_length(1, &self))?;

                let structure = value
                    .remove("type")
                    .ok_or_else(|| de::Error::missing_field("type"))?;

                Ok(Util::TopBitSetTerminatedArray(Box::new(structure)))
            }
            unknown_variant => {
                // This is what happens when the nodejs developers write a "cool" spec.
                let loop_util: Loop = seq.next_element()?.ok_or_else(|| {
                    de::Error::unknown_variant(
                        unknown_variant,
                        &["buffer", "mapper", "bitfield", "pstring"],
                    )
                })?;

                Ok(Util::Loop(Box::new(loop_util)))
            }
        }
    }
}

impl<'de> Deserialize<'de> for Util {
    fn deserialize<D>(deserializer: D) -> Result<Self, <D>::Error>
    where
        D: Deserializer<'de>,
    {
        deserializer.deserialize_seq(UtilVisitor)
    }
}


