using UnityEngine;
using System.Collections;

public interface ReactsToCompound {

    // Interface method for GameObjects that will react to one or more types of compounds
    // This method should check for the type of compound (Water, for instance) and, if it should react,
    // performs the appropriate reaction.
    void reactToCompound();
}
