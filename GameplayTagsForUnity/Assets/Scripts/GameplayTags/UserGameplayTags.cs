
namespace werignac.GameplayTags {
	public sealed class UserGameplayTags {
		public sealed class Action : GameplayTag {
			public sealed class Movement : GameplayTag {
				public sealed class Spin : GameplayTag {
					public static string StaticName {
						get { return "Spin"; }
					}
					public static GameplayTag StaticParent {
						get { return new Movement(); }
					}
					public static GameplayTag[] StaticChildren {
						get { return new GameplayTag[] {  }; }
					}
					protected override string GetName() {
						 return StaticName;
					}
					protected override GameplayTag GetParent() {
						 return StaticParent;
					}
					protected override GameplayTag[] GetChildren() {
						 return StaticChildren;
					}
				}

				public sealed class Jump : GameplayTag {
					public static string StaticName {
						get { return "Jump"; }
					}
					public static GameplayTag StaticParent {
						get { return new Movement(); }
					}
					public static GameplayTag[] StaticChildren {
						get { return new GameplayTag[] {  }; }
					}
					protected override string GetName() {
						 return StaticName;
					}
					protected override GameplayTag GetParent() {
						 return StaticParent;
					}
					protected override GameplayTag[] GetChildren() {
						 return StaticChildren;
					}
				}

				public sealed class Crouch : GameplayTag {
					public static string StaticName {
						get { return "Crouch"; }
					}
					public static GameplayTag StaticParent {
						get { return new Movement(); }
					}
					public static GameplayTag[] StaticChildren {
						get { return new GameplayTag[] {  }; }
					}
					protected override string GetName() {
						 return StaticName;
					}
					protected override GameplayTag GetParent() {
						 return StaticParent;
					}
					protected override GameplayTag[] GetChildren() {
						 return StaticChildren;
					}
				}

				public static string StaticName {
					get { return "Movement"; }
				}
				public static GameplayTag StaticParent {
					get { return new Action(); }
				}
				public static GameplayTag[] StaticChildren {
					get { return new GameplayTag[] { new Crouch(), new Jump(), new Spin() }; }
				}
				protected override string GetName() {
					 return StaticName;
				}
				protected override GameplayTag GetParent() {
					 return StaticParent;
				}
				protected override GameplayTag[] GetChildren() {
					 return StaticChildren;
				}
			}

			public sealed class Interaction : GameplayTag {
				public sealed class Open : GameplayTag {
					public static string StaticName {
						get { return "Open"; }
					}
					public static GameplayTag StaticParent {
						get { return new Interaction(); }
					}
					public static GameplayTag[] StaticChildren {
						get { return new GameplayTag[] {  }; }
					}
					protected override string GetName() {
						 return StaticName;
					}
					protected override GameplayTag GetParent() {
						 return StaticParent;
					}
					protected override GameplayTag[] GetChildren() {
						 return StaticChildren;
					}
				}

				public sealed class Inspect : GameplayTag {
					public static string StaticName {
						get { return "Inspect"; }
					}
					public static GameplayTag StaticParent {
						get { return new Interaction(); }
					}
					public static GameplayTag[] StaticChildren {
						get { return new GameplayTag[] {  }; }
					}
					protected override string GetName() {
						 return StaticName;
					}
					protected override GameplayTag GetParent() {
						 return StaticParent;
					}
					protected override GameplayTag[] GetChildren() {
						 return StaticChildren;
					}
				}

				public sealed class Close : GameplayTag {
					public static string StaticName {
						get { return "Close"; }
					}
					public static GameplayTag StaticParent {
						get { return new Interaction(); }
					}
					public static GameplayTag[] StaticChildren {
						get { return new GameplayTag[] {  }; }
					}
					protected override string GetName() {
						 return StaticName;
					}
					protected override GameplayTag GetParent() {
						 return StaticParent;
					}
					protected override GameplayTag[] GetChildren() {
						 return StaticChildren;
					}
				}

				public static string StaticName {
					get { return "Interaction"; }
				}
				public static GameplayTag StaticParent {
					get { return new Action(); }
				}
				public static GameplayTag[] StaticChildren {
					get { return new GameplayTag[] { new Close(), new Inspect(), new Open() }; }
				}
				protected override string GetName() {
					 return StaticName;
				}
				protected override GameplayTag GetParent() {
					 return StaticParent;
				}
				protected override GameplayTag[] GetChildren() {
					 return StaticChildren;
				}
			}

			public static string StaticName {
				get { return "Action"; }
			}
			public static GameplayTag StaticParent {
				get { return null; }
			}
			public static GameplayTag[] StaticChildren {
				get { return new GameplayTag[] { new Interaction(), new Movement() }; }
			}
			protected override string GetName() {
				 return StaticName;
			}
			protected override GameplayTag GetParent() {
				 return StaticParent;
			}
			protected override GameplayTag[] GetChildren() {
				 return StaticChildren;
			}
		}
		public sealed class Item : GameplayTag {
			public sealed class Weapon : GameplayTag {
				public sealed class Ranged : GameplayTag {
					public sealed class Short : GameplayTag {
						public static string StaticName {
							get { return "Short"; }
						}
						public static GameplayTag StaticParent {
							get { return new Ranged(); }
						}
						public static GameplayTag[] StaticChildren {
							get { return new GameplayTag[] {  }; }
						}
						protected override string GetName() {
							 return StaticName;
						}
						protected override GameplayTag GetParent() {
							 return StaticParent;
						}
						protected override GameplayTag[] GetChildren() {
							 return StaticChildren;
						}
					}

					public sealed class Medium : GameplayTag {
						public static string StaticName {
							get { return "Medium"; }
						}
						public static GameplayTag StaticParent {
							get { return new Ranged(); }
						}
						public static GameplayTag[] StaticChildren {
							get { return new GameplayTag[] {  }; }
						}
						protected override string GetName() {
							 return StaticName;
						}
						protected override GameplayTag GetParent() {
							 return StaticParent;
						}
						protected override GameplayTag[] GetChildren() {
							 return StaticChildren;
						}
					}

					public sealed class Long : GameplayTag {
						public static string StaticName {
							get { return "Long"; }
						}
						public static GameplayTag StaticParent {
							get { return new Ranged(); }
						}
						public static GameplayTag[] StaticChildren {
							get { return new GameplayTag[] {  }; }
						}
						protected override string GetName() {
							 return StaticName;
						}
						protected override GameplayTag GetParent() {
							 return StaticParent;
						}
						protected override GameplayTag[] GetChildren() {
							 return StaticChildren;
						}
					}

					public static string StaticName {
						get { return "Ranged"; }
					}
					public static GameplayTag StaticParent {
						get { return new Weapon(); }
					}
					public static GameplayTag[] StaticChildren {
						get { return new GameplayTag[] { new Long(), new Medium(), new Short() }; }
					}
					protected override string GetName() {
						 return StaticName;
					}
					protected override GameplayTag GetParent() {
						 return StaticParent;
					}
					protected override GameplayTag[] GetChildren() {
						 return StaticChildren;
					}
				}

				public sealed class Mixed : GameplayTag {
					public static string StaticName {
						get { return "Mixed"; }
					}
					public static GameplayTag StaticParent {
						get { return new Weapon(); }
					}
					public static GameplayTag[] StaticChildren {
						get { return new GameplayTag[] {  }; }
					}
					protected override string GetName() {
						 return StaticName;
					}
					protected override GameplayTag GetParent() {
						 return StaticParent;
					}
					protected override GameplayTag[] GetChildren() {
						 return StaticChildren;
					}
				}

				public sealed class Melee : GameplayTag {
					public static string StaticName {
						get { return "Melee"; }
					}
					public static GameplayTag StaticParent {
						get { return new Weapon(); }
					}
					public static GameplayTag[] StaticChildren {
						get { return new GameplayTag[] {  }; }
					}
					protected override string GetName() {
						 return StaticName;
					}
					protected override GameplayTag GetParent() {
						 return StaticParent;
					}
					protected override GameplayTag[] GetChildren() {
						 return StaticChildren;
					}
				}

				public static string StaticName {
					get { return "Weapon"; }
				}
				public static GameplayTag StaticParent {
					get { return new Item(); }
				}
				public static GameplayTag[] StaticChildren {
					get { return new GameplayTag[] { new Melee(), new Mixed(), new Ranged() }; }
				}
				protected override string GetName() {
					 return StaticName;
				}
				protected override GameplayTag GetParent() {
					 return StaticParent;
				}
				protected override GameplayTag[] GetChildren() {
					 return StaticChildren;
				}
			}

			public sealed class Consumable : GameplayTag {
				public sealed class Potion : GameplayTag {
					public static string StaticName {
						get { return "Potion"; }
					}
					public static GameplayTag StaticParent {
						get { return new Consumable(); }
					}
					public static GameplayTag[] StaticChildren {
						get { return new GameplayTag[] {  }; }
					}
					protected override string GetName() {
						 return StaticName;
					}
					protected override GameplayTag GetParent() {
						 return StaticParent;
					}
					protected override GameplayTag[] GetChildren() {
						 return StaticChildren;
					}
				}

				public sealed class PartyPopper : GameplayTag {
					public static string StaticName {
						get { return "PartyPopper"; }
					}
					public static GameplayTag StaticParent {
						get { return new Consumable(); }
					}
					public static GameplayTag[] StaticChildren {
						get { return new GameplayTag[] {  }; }
					}
					protected override string GetName() {
						 return StaticName;
					}
					protected override GameplayTag GetParent() {
						 return StaticParent;
					}
					protected override GameplayTag[] GetChildren() {
						 return StaticChildren;
					}
				}

				public sealed class Grenade : GameplayTag {
					public static string StaticName {
						get { return "Grenade"; }
					}
					public static GameplayTag StaticParent {
						get { return new Consumable(); }
					}
					public static GameplayTag[] StaticChildren {
						get { return new GameplayTag[] {  }; }
					}
					protected override string GetName() {
						 return StaticName;
					}
					protected override GameplayTag GetParent() {
						 return StaticParent;
					}
					protected override GameplayTag[] GetChildren() {
						 return StaticChildren;
					}
				}

				public static string StaticName {
					get { return "Consumable"; }
				}
				public static GameplayTag StaticParent {
					get { return new Item(); }
				}
				public static GameplayTag[] StaticChildren {
					get { return new GameplayTag[] { new Grenade(), new PartyPopper(), new Potion() }; }
				}
				protected override string GetName() {
					 return StaticName;
				}
				protected override GameplayTag GetParent() {
					 return StaticParent;
				}
				protected override GameplayTag[] GetChildren() {
					 return StaticChildren;
				}
			}

			public static string StaticName {
				get { return "Item"; }
			}
			public static GameplayTag StaticParent {
				get { return null; }
			}
			public static GameplayTag[] StaticChildren {
				get { return new GameplayTag[] { new Consumable(), new Weapon() }; }
			}
			protected override string GetName() {
				 return StaticName;
			}
			protected override GameplayTag GetParent() {
				 return StaticParent;
			}
			protected override GameplayTag[] GetChildren() {
				 return StaticChildren;
			}
		}

		public static GameplayTag[] RootTags {
			get { return new GameplayTag[] {new Action(), new Item()}; }
		}
	}
}