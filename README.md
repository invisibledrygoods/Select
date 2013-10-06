Select
======

Select adds [CSS selectors](http://api.jquery.com/category/selectors/basic-css-selectors/)
to [Unity](http://unity3d.com/), as well as a few [jQueryish](http://api.jquery.com/) features.

If you don't know what CSS or jQuery are then think of it like `List<GameObject>` on crack
(and then go [learn you some school](http://learn.jquery.com/about-jquery/how-jquery-works/)).

Examples
--------

Show off your amazing abs:

    new Select("#player shirt").Hide();

Delete all cowards:

    new Select("#Throthgar, #Bothfar, #Ainar").Destroy();
    
Paint all allies' weapons red with blood:

    new Select("ally .Weapon").Each<Renderer>(renderer => renderer.material.color = Color.red);

Knock all wimpy foes away from this accursed place:

    foreach (RigidBody enemy in new Select("enemy.Wimpy").Get<RigidBody>()) {
        Vector3 away = enemy.transform.position - transform.position;
        enemy.AddForce(away.normalized * 10 / away.magnitude);
    }

Bring prosperity to the city once again:

    new Select("npc.ShopKeeper").Each<ShopKeeper>(shopkeeper => shopkeeper.Restock());

Reopen the castle and close the barracks:

    new Select("castle .Door:!open").Each<Door>(door => door.Open());
    new Select("barracks .Door:open").Each<Door>(door => door.Close());

Levy a tax against your new domain:

    new Select("npc.ShopKeeper[gold=0]").Each<ShopKeeper>(shop => shop.Warn());
    new Select("npc.ShopKeeper[gold<0]").Each<ShopKeeper>(shop => shop.Foreclose());

Translating selectors between HTML and Unity
--------------------------------------------

| HTML/CSS     | Unity         |
|--------------|---------------|
| tag          | tag           |
| .class       | .Component    |
| #id          | #name         |
| :state       | :boolean      |
| [attr=value] | [IComparable] |

Tests
-----

Tests can be run by finding and launching the scene SelectTestRunner

Most of the tests were also
[rewritten to run against jQuery](https://rawgithub.com/invisibledrygoods/Select/master/jQueryTest.html)
(note: these tests are for comparison, failing tests demonstrate differences, not bugs)

If you want to use a testing framework like [GivenWhen](https://github.com/invisibledrygoods/GivenWhenUnity)
along with Select then it is recommended that you wrap your selectors in a `MonoBehaviour`, and write a mock
Select that allows you to inject fake results.

In Progress
-----------

__Splat:__

    new Select("*"); // select everything

__Has meta-selector:__

These seems really hard. Probably the best bet would be to make a second Select for the inner query then
prune any selections from the outer query that don't have at least one element of the inner query in their
heirarchy.

    // deal damage to every mob that has a cursed sword somewhere in their heirarchy
    new Select(".Mob:has(.CursedSword)").Each<Mob>(mob => mob.DealDamage(5));

If you need a feature urgently hit me up and I can work on it.
