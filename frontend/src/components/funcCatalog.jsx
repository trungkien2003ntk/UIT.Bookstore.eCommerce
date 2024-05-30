export const findParent = (objects, objectId) => {
  const parents = []
  let currentObjectId = objectId
  let parentObject = objects.find((obj) => obj.id === currentObjectId)

  while (parentObject && parentObject.parentProductTypeId !== null) {
    currentObjectId = parentObject.parentProductTypeId
    parentObject = objects.find((obj) => obj.id === currentObjectId)
    if (parentObject) {
      parents.push(parentObject)
    }
  }

  parents.sort((a, b) => a.level - b.level)

  return parents
}
