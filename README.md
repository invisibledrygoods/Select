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

    foreach (Renderer renderer in new Select("ally .Weapon").renderers) {
        renderer.material.color = Color.red;
    }

Knock all wimpy foes away from this accursed place:

    foreach (RigidBody enemy in new Select("enemy.Wimpy").rigidbodies) {
        Vector3 away = enemy.transform.position - transform.position;
        enemy.AddForce(away.normalized * 10 / away.magnitude);
    }

Bring prosperity to the city once again:

    foreach (ShopKeeper shopkeeper in new Select("npc.ShopKeeper").Get<ShopKeeper>()) {
      shopkeeper.Restock();
    }

Translating selectors between HTML and Unity
--------------------------------------------

| HTML/CSS | Unity      |
|----------|------------|
| tag      | tag        |
| .class   | .Component |
| #id      | #name      |

Note: There's no meta-selectors or selectors for layers yet. We'll probably add them as we need them.
Remember to give us a pull request if you add any (or just a normal request if you really want one but don't
know how to implement it).

Tests
-----

Tests can be run by finding and launching the scene SelectTestRunner

The tests were also
[rewritten to run against jQuery](https://rawgithub.com/invisibledrygoods/Select/master/jQueryTest.html)
(note: these tests are for comparison, failing tests demonstrate differences, not flaws)

If you want to use a testing framework like [GivenWhen](https://github.com/invisibledrygoods/GivenWhenUnity)
along with Select then it is recommended that you wrap your selectors in a `MonoBehaviour`, and write a mock
Select that allows you to inject fake results.
