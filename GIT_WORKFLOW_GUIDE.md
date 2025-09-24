# Guía de Flujo de Trabajo Git - PsychoShare Network API

## ¿Cómo se elimina un PR? (How to delete a PR?)

### Respuesta Rápida
**No se puede "eliminar" un PR**, pero se puede **cerrar** o **convertir en borrador**. Aquí están las opciones:

## 🔧 Opciones para Manejar PRs Problemáticos

### 1. Cerrar el PR
```bash
# En GitHub web interface:
# - Ve al PR → "Close pull request" button
# - O comenta "/close" si tienes permisos
```

### 2. Convertir a Borrador (Draft)
```bash
# En GitHub web interface:
# - Ve al PR → "Convert to draft" en la barra lateral
```

### 3. Eliminar la Branch Localmente
```bash
# Si quieres eliminar tu branch local:
git branch -d nombre-de-tu-branch

# Para forzar la eliminación:
git branch -D nombre-de-tu-branch
```

### 4. Eliminar la Branch Remota
```bash
# Eliminar branch remota:
git push origin --delete nombre-de-tu-branch
```

## 🎯 Problema Actual: PR Apuntando a `main`

### ¿Por qué está apuntando a `main`?
Según el historial del repositorio, el flujo de trabajo correcto debería ser:

```
main ← development ← feature-branches
```

### Branches Correctos para Targeting:
- ✅ **`development`** o **`development2`** - Para nuevas características
- ❌ **`main`** - Solo para releases estables

## 📋 Flujo de Trabajo Correcto

### 1. Crear Feature Branch
```bash
# Partir desde development, no desde main
git checkout development
git pull origin development
git checkout -b feature/nombre-descriptivo
```

### 2. Hacer Commits
```bash
git add .
git commit -m "feat: descripción clara en inglés"
```

### 3. Push y PR
```bash
git push origin feature/nombre-descriptivo
# Crear PR hacia 'development' branch, NO hacia 'main'
```

### 4. Después del Merge
```bash
# Limpiar branch local
git checkout development
git pull origin development
git branch -d feature/nombre-descriptivo
```

## 🚨 Para el PR #15 Actual

### Problema Identificado:
- El PR está apuntando a `main` 
- Debería apuntar a `development` o `development2`

### Soluciones:

#### Opción 1: Cambiar el Target Branch
1. Ve al PR en GitHub
2. Click en "Edit" junto al título
3. Cambia el target branch de `main` a `development2`

#### Opción 2: Cerrar y Recrear
1. Cierra este PR
2. Crea un nuevo PR hacia `development2`
3. Referencia el PR anterior en la descripción

#### Opción 3: Cerrar si es Duplicado
Si el trabajo ya se hizo en otro PR (como #14), simplemente cierra este PR con un comentario explicativo.

## 📝 Convenciones del Proyecto

### Branch Naming (Según README.md):
```bash
✅ Correcto:
- feature/user-authentication
- feature/file-upload-feature  
- bugfix/login-validation

❌ Incorrecto:
- feature/john-user-login
- main-feature
- test-branch
```

### Commit Messages:
```bash
✅ Correcto:
- "feat: add user authentication system"
- "fix: resolve login validation issue"  
- "docs: update API documentation"

❌ Incorrecto:
- "changes"
- "update"
- "arreglo"
```

### PR Targets:
```bash
✅ Correcto:
- feature/* → development
- hotfix/* → main (solo emergencias)
- docs/* → development

❌ Incorrecto:  
- feature/* → main
- cualquier-branch → main (sin review)
```

## 🔄 Estados de PR y Acciones

| Estado | Acción Disponible | Comando/UI |
|--------|-------------------|------------|
| Open | Cerrar | "Close pull request" button |
| Open | Convertir a draft | "Convert to draft" |
| Draft | Marcar como ready | "Ready for review" |
| Merged | ❌ No se puede deshacer | Crear revert PR |
| Closed | Reabrir | "Reopen pull request" |

## 🎯 Recomendación Específica

Para el PR actual (#15):

1. **Revisar si el contenido ya existe en PR #14**
2. **Si es duplicado**: Cerrar con comentario "Duplicate of #14"  
3. **Si es diferente**: Cambiar target branch a `development2`
4. **Si es incorrecto**: Cerrar y crear nuevo PR con branch correcto

## 🔗 Enlaces Útiles

- [Documentación Git Flow](https://nvie.com/posts/a-successful-git-branching-model/)
- [GitHub PR Guidelines](https://docs.github.com/en/pull-requests)
- [Conventional Commits](https://www.conventionalcommits.org/)

---

**Nota**: Este documento está en español para el equipo, pero recuerda que todos los commits y código deben estar en inglés según las pautas del proyecto.